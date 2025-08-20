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

    public void StartBattle(List<GameObject> monsters)
    {
        _battleGround.SetActive(true);
        
        RegisterPlayer();
        RegisterMonster(monsters); 
        
        Camera.main.transform.SetParent(_battleGround.transform);
        _progressCombat = true;
    }

    public void EndBattle()
    {
        _battleGround.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState != GameManager.EGameState.Event)
            return;

        if (_progressCombat == false)
            return;
        
        // Player 공격 턴

        if (_player.IsAttack())
        {
            
            float aocDamage = 0f;
            float damage = _player.GetDamage(out aocDamage);
            
            Debug.Log($"플레이어 공격");
            
            _monsters.First().TakeDamage(damage);

            foreach (var monster in _monsters)
            {
                monster.TakeDamage(aocDamage);
            }
        }

        List<Monster> monstersToRemove = new List<Monster>();

        foreach (Monster monster in _monsters)
        {
            if (monster.IsDead)
            {
                monstersToRemove.Add(monster);
            }
        }

        foreach (Monster monster in monstersToRemove)
        {
            _monsters.Remove(monster);
            
            Debug.Log($"{monster.name} 사망");
            
            Destroy(monster.gameObject);
        }

        if (_monsters.Count == 0)
        {
            // Player 승리 시나리오
            GameManager.Instance.OnBattleEnd(true);
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
            GameManager.Instance.OnBattleEnd(false);
        }
    }

    private void RegisterPlayer()
    {
        _player = TileManager.Instance.Player.GetComponent<Player>();
        
        _player.transform.SetParent(_battleGround.transform);
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        _player.Init();
    }
    
    private void RegisterMonster(List<GameObject> monsters)
    {
        _monsters = new List<Monster>();
        for (int i = 0; i < monsters.Count; i++)
        {
            GameObject monster = Instantiate(monsters[i], _battleGround.transform);
            monster.transform.localPosition = new Vector3(0.5f * (i + 1), 0.5f * (i % 2 - 1), 6);
            _monsters.Add(monster.GetComponent<Monster>());
            monster.GetComponent<Monster>().Init();
        }
    }
}
