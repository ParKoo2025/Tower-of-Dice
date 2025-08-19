using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    [SerializeField] private List<StatScriptable> _equipmentStat = new List<StatScriptable>();

    public Stat TotalStat { get; private set; } = new Stat();

    public void SetEquipment(EEquipmentType equipmentType, StatScriptable equipmentStat)
    {
        _equipmentStat[(int)equipmentType] = equipmentStat;
        CalculateStat();
    }    
    
    /// <summary>
    /// TotalStat을 계산한다.
    /// </summary>
    private void CalculateStat()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            float sum = _basicStat[(EStatType)i];
            for (int j = 0; j < (int)EEquipmentType.Size; j++)
            {
                sum += _equipmentStat[j][(EStatType)i];
            }

            TotalStat[(EStatType)i] = sum;
        }
    }

    private void Start()
    {
        CalculateStat();
    }
}
