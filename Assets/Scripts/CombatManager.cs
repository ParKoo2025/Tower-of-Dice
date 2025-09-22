
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : SingletonBehavior<CombatManager>
{
    [SerializeField] private GameObject _battleGround;
    
    private Player _player;
    private List<Monster> _monsters;

    private float _combatTime;
    
    private bool _progressCombat = false;

    public Action<Transform, EDamageType, float> OnChangeHealth;
    
    public int MonsterCount => _monsters.Count;
    
    public void StartBattle(List<GameObject> monsters)
    {
        _battleGround.SetActive(true);
        
        Camera.main.transform.SetParent(_battleGround.transform, false);
        RegisterPlayer();
        RegisterMonster(monsters); 
        
        _progressCombat = true;
        _combatTime = 0.0f;
        
        PassiveBus.Publish(EPassiveTriggerType.OnBattleStart);
    }

    public void EndBattle()
    { 
        _progressCombat = false;
        
        _player.StopAttack();
        _player.SetHpControllerActive(false);
        foreach (var monster in _monsters)
        {
            monster.StopAttack();
            Destroy(monster.gameObject);
        }
        _battleGround.SetActive(false);
    }

    public void ProcessPlayerAttack(EDamageType damageType, float damage, float aocDamage)
    {
        if (_monsters.Count > 0 && damage > 0)
        {
            PassiveBus.Publish(EPassiveTriggerType.OnPlayerAttack);
            _monsters.First().TakeDamage(damageType, damage);
        }

        if (aocDamage > 0)
        {
            foreach (var monster in _monsters)
            { 
                monster.TakeDamage(EDamageType.Attack, aocDamage);
            }
        }

        ProcessDeadMonsters();
    }

    public void ProcessMonsterAttack(EDamageType damageType, float damage, float aocDamage)
    {
        _player.TakeDamage(damageType, damage);

        if (_player.IsDead)
        {
            ProcessDeadPlayer();
        }
    }

    public bool IsPastTime(float time)
    {
        return _combatTime < time;
    }

    private void Update()
    {
        _combatTime += Time.deltaTime;
    }

    private void RegisterPlayer()
    {
        _player = TileManager.Instance.Player.GetComponent<Player>();
        _player.transform.SetParent(_battleGround.transform, false);
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        _player.transform.localScale = Vector3.one;
        _player.transform.GetChild(0).transform.localScale = new Vector3(-1f, 1f, 1f);
        _player.ProcessAttack += ProcessPlayerAttack;
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
            monster.ProcessAttack += ProcessMonsterAttack;
            monster.StartAttack();
        }
    }

    private void ProcessDeadPlayer()
    {
        PassiveBus.Publish(EPassiveTriggerType.OnPlayerDeath);

        // Passive 처리가 끝났는데도 여전히 죽어있다면 
        if (_player.IsDead)
        {
            GameManager.Instance.OnBattleEnd(false);
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
            
            // 몬스터 죽었을 때 패시브 트리거 발동
            PassiveBus.Publish(EPassiveTriggerType.OnMonsterDeath);
        }

        if (_monsters.Count == 0)
        {
            GameManager.Instance.OnBattleEnd(true);
            _player.StopAttack();
        }
    }

}
