using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum EStatType
{
    Health, HealthRegen, HealthStealRate,
    AttackDamage, AttackSpeed, AocDamage,
    CriticalRate, CriticalMultiplier,
    Defence, EvasionRate, CounterAttackRate,
    Size
}

[System.Serializable]
public class Stat
{
    [SerializeField]
    [TableList(ShowIndexLabels = false)]
    [LabelText("Stats")]
    private StatData[] statsData = new StatData[(int)EStatType.Size];

    public Stat()
    {
        InitializeStatsData();
    }

    private void InitializeStatsData()
    {
        if (statsData == null || statsData.Length != (int)EStatType.Size)
        {
            statsData = new StatData[(int)EStatType.Size];
        }
        
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            if (statsData[i] == null)
            {
                statsData[i] = new StatData((EStatType)i, 0f);
            }
        }
    }

    public float this[EStatType type]
    {
        get 
        {
            int index = (int)type;
            if (index >= 0 && index < statsData.Length)
                return statsData[index].Value;
            return 0f;
        }
        set 
        {
            int index = (int)type;
            if (index >= 0 && index < statsData.Length)
                statsData[index].Value = value;
        }
    }

    [System.Serializable]
    public class StatData
    {
        [HideLabel, DisplayAsString, HorizontalGroup("Group", 0.4f)]
        public string Name;

        [HideLabel, HorizontalGroup("Group", 0.6f)]
        public float Value;

        public StatData(EStatType type, float value)
        {
            Name = type.ToString();
            Value = value;
        }
    }

    // Inspector에서 배열 크기가 변경되는 것을 방지
    [OnInspectorInit]
    private void OnInspectorInit()
    {
        InitializeStatsData();
    }
}