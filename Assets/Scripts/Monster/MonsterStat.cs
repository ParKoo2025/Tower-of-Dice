using System;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;

    public Stat TotalStat { get; private set; } = new Stat();
    public float CurrentHealth { get; set; } = 0.0f;
    public void SetStat(StatScriptable stat)
    {
        _basicStat = stat;
    }

    private void Awake()
    {
        CalculateStat();
        CurrentHealth = TotalStat[EStatType.Health];
    }

    private void CalculateStat()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            TotalStat[(EStatType)i] = _basicStat[(EStatType)i];
        }
    }
}
