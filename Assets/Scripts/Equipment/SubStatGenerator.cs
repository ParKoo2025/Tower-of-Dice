using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RarityStats
{
    public ERarity _rarity;
    public List<SubStats> _subStats;
}

[Serializable]
public struct SubStats
{
    public EStatType _statType;
    [Range(0f, 1000f)] public float _min;
    [Range(0f, 1000f)] public float _max;
}

[Serializable]
public struct MainStats
{
    public EEquipments _equipmentName;
    public EStatType _statType;
    public float _value;
}

public class SubStatGenerator : SingletonBehavior<SubStatGenerator>
{
    [SerializeField] RarityStats[] _rarityStats;
    [SerializeField] MainStats[] _mainStats;
    
    private List<(EStatType stat, SubStats subStat)> _subStats;

    public List<(EStatType, float)> StatGenerator(ERarity rarity, EEquipments equipmentName)
    {
        var result = new List<(EStatType, float)>();
        
        result.Add((_mainStats[(int)equipmentName]._statType, _mainStats[(int)equipmentName]._value));
        
        int num = Math.Min((int)rarity, 3);

        if (num != 0)
        {
            Debug.Log("substat 생성");
            int idx = Array.FindIndex(_rarityStats, rs => rs._rarity == rarity);
            var pool = _rarityStats[idx]._subStats;

            for (int i = 0; i < num; i++)
            {
                Debug.Log("substat 선택중");

                int pick = UnityEngine.Random.Range(0, pool.Count);
                var template = pool[pick];

                float rolledValue = UnityEngine.Random.Range(template._min, template._max);
                
                Debug.Log("substat 선택중");

                result.Add((template._statType, (int)rolledValue));
            }
        }
        
        return result;
    }
}
