using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : SingletonBehavior<CombatManager>
{
    [SerializeField] private GameObject _battleGround;
    
    private Player _player;
    private List<Monster> _monsters;
    
    private bool _progressCombat = false;

    public event Action<Transform, float> OnDamaged;

    public void StartBattle(List<GameObject> monsters)
    {
        _battleGround.SetActive(true);
        
        Camera.main.transform.SetParent(_battleGround.transform, false);
        RegisterPlayer();
        RegisterMonster(monsters); 
        
        _progressCombat = true;
    }

    public void EndBattle()
    {
        _progressCombat = false;
        _battleGround.SetActive(false);
        
        _player.StopAttack();
        foreach (var monster in _monsters)
        {
            monster.StopAttack();
            Destroy(monster.gameObject);
        }
    }

    public int ProcessPlayerAttack(float damage, float aocDamage)
    {
        int aocCount = 0;
        if (_monsters.Count > 0)
        {
            OnDamaged?.Invoke(_monsters.First().transform, damage);
            _monsters.First().TakeDamage(damage);
        }

        if (aocDamage > 0)
        {
            foreach (var monster in _monsters)
            {
                OnDamaged?.Invoke(monster.transform, damage);
                monster.TakeDamage(aocDamage);
                aocCount++;
            }
        }

        ProcessDeadMonsters();
        return aocCount;
    }

    public void ProcessMonsterAttack(Monster attacker, float damage)
    {
        OnDamaged?.Invoke(_player.transform, damage);
        _player.TakeDamage(damage);

        if (_player.IsDead)
        {
            GameManager.Instance.OnBattleEnd(false);
        }
    }

    private void RegisterPlayer()
    {
        _player = TileManager.Instance.Player.GetComponent<Player>();
        _player.transform.SetParent(_battleGround.transform, false);
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        _player.transform.localScale = Vector3.one;
        _player.transform.GetChild(0).transform.localScale = new Vector3(-1f, 1f, 1f);
        _player.StartAttack();
    }
    
    private void RegisterMonster(List<GameObject> monsters)
    {
        _monsters = new List<Monster>();
        for (int i = 0; i < monsters.Count; i++)
        {
            var monster = Instantiate(monsters[i], _battleGround.transform, false).GetComponent<Monster>();
            monster.transform.localPosition = new Vector3(0.5f * (i + 1), 0.5f * (i % 2 - 1), -1f);
            _monsters.Add(monster);
            monster.StartAttack();
        }
    }
    
    private void ProcessDeadMonsters()
    {
        List<Monster> monstersToRemove = new List<Monster>();

        foreach (var monster in _monsters)
        {
            if (monster.IsDead)
            {
                monstersToRemove.Add(monster);
            }
        }

        foreach (var monster in monstersToRemove)
        {
            _monsters.Remove(monster);
            print($"{monster.name} 사망");
           Destroy(monster.gameObject, 2f);
        }

        if (_monsters.Count == 0)
        {
            GameManager.Instance.OnBattleEnd(true);
            _player.StopAttack();
        }
    }


}
