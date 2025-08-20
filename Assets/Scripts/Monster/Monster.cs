using System;
using UnityEngine;

public class Monster : MonoBehaviour, ICombatant
{
    private MonsterStat _monsterStat;
    
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

        if (sum >= _monsterStat.TotalStat[EStatType.AttackSpeed])
        {
            sum = 0.0f;
            return true;
        }

        return false;
    }
    
    public float GetDamage(out float aocDamage)
    {
        aocDamage = 0.0f;
        return _monsterStat.TotalStat[EStatType.AttackDamage];
    }

    public void TakeDamage(float damage)
    {
        _monsterStat.TotalStat[EStatType.Health] -= damage;
        
        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_monsterStat.TotalStat[EStatType.Health]})");
        
        if (_monsterStat.TotalStat[EStatType.Health] <= 0.0f)
            IsDead = true;
    }

    private void Awake()
    {
        _monsterStat = GetComponent<MonsterStat>();
    }

    public void SetStat(StatScriptable stat)
    {
        _monsterStat.SetStat(stat);
    }



    
}
