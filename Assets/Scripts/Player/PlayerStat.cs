using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    [Header("장비 슬롯 (타입별 1칸)")]
    [SerializeField] private Equipment[] _equipped = new Equipment[(int)EEquipmentType.Size];

    public Stat TotalStat { get; private set; } = new Stat();

    public void SetEquipment(Equipment eq)
    {
        var idx = (int)eq.EquipmentType;
        if (_equipped[idx] != null) UnEquip(idx);
        _equipped[idx] = eq;
        AddEquipmentStat(idx);
    }

    private void UnEquip(int idx)
    {
        SubtractEquipmentStat(idx);
        _equipped[idx] = null;
    }

    private void Start()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            TotalStat[(EStatType)i] = _basicStat[(EStatType)i];
        }
    }
    
    /// <summary>
    /// TotalStat을 계산한다.
    /// </summary>
    private void AddEquipmentStat(int idx)
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            TotalStat[(EStatType)i] += _equipped[idx].Stat[(EStatType)i];
        }
    }
    
    private void SubtractEquipmentStat(int idx)
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            TotalStat[(EStatType)i] -= _equipped[idx].Stat[(EStatType)i];
        }
    }
}
