using System;
using UnityEngine;

public class Player : MonoBehaviour, ICombatant
{
    private PlayerStat _playerStat;
    private float sum = 0.0f;
    
    public bool IsDead { get; private set; }
    public void Init()
    {
        sum = 0.0f;
        IsDead = false;
    }

    public bool IsAttack()
    {
        sum += Time.deltaTime;

        if (sum >= _playerStat.TotalStat[EStatType.AttackSpeed])
        {
            sum = 0.0f;
            return true;
        }

        return false;
    }

    public float GetDamage(out float aocDamage)
    {
        aocDamage = _playerStat.TotalStat[EStatType.AocDamage];
        return _playerStat.TotalStat[EStatType.AttackDamage];
    }

    public void TakeDamage(float damage)
    {
        _playerStat.TotalStat[EStatType.Health] -= damage;
        
        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_playerStat.TotalStat[EStatType.Health]})");

        
        if (_playerStat.TotalStat[EStatType.Health] <= 0.0f)
            IsDead = true;
    }

    private void Awake()
    {
        _playerStat = GetComponent<PlayerStat>();
    }
}
