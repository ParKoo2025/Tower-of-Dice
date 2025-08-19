using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : SingletonBehavior<CombatManager>
{
    private enum ECombatState
    {
        Idle,
        Progress
    }

    private ECombatState _combatState = ECombatState.Idle;

    private ICombatant _player;
    private List<ICombatant> _monsters;

    public void RegisterMonster(List<ICombatant> _monsters)
    {
        
    }    
    
    private void Update()
    {
        if (_combatState != ECombatState.Progress)
            return;

        // Player 공격 턴

        if (_player.IsAttack())
        {
            float aocDamage = 0f;
            float damage = _player.GetDamage(out aocDamage);

            _monsters.First().TakeDamage(damage);

            foreach (var monster in _monsters)
            {
                monster.TakeDamage(aocDamage);
            }
        }

        _monsters.RemoveAll(c => c.IsDead);

        if (_monsters.Count == 0)
        {
            // Player 승리 시나리오
            return;
        }

        // Monster 공격 턴

        foreach (var monster in _monsters)
        {
            if (monster.IsAttack())
            {
                // 몬스터에선 필요 없긴 함
                float aocDamage = 0f;

                float damage = monster.GetDamage(out aocDamage);

                _player.TakeDamage(damage);
            }
        }

        if (_player.IsDead)
        {
            // 몬스터 승리 시나리오
        }
    }
}
