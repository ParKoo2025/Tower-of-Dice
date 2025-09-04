using System.Collections.Generic;
using UnityEngine;

public class CombatantStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    [Header("장비 슬롯 (타입별 1칸)")]
    [SerializeField] private Equipment[] _equipped = new Equipment[(int)EEquipmentType.Size];

    public Stat Stat { get; } = new Stat();
    public float CurrentHealth { get; set; }

    public void SetEquipment(Equipment eq)
    {
        var idx = (int)eq.EquipmentType;
        if (_equipped[idx] != null) UnEquip(idx);
        _equipped[idx] = eq;
        AddEquipmentStat(idx);
    }

    private void Awake()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            Stat[(EStatType)i] = _basicStat[(EStatType)i];
        }
        CurrentHealth = Stat[EStatType.Health];
    }
    
    private void UnEquip(int idx)
    {
        SubtractEquipmentStat(idx);
        _equipped[idx].OnDestroyEquipment();
        _equipped[idx] = null;
    }

    public void ReCalculateEquipmentStats()
    {
        for (int j = 0; j < (int)EEquipmentType.Size; ++j)
        {
            if (_equipped[j] != null)
            {
                SubtractEquipmentStat(j);
                _equipped[j].ApplyFloorPenalty();
                AddEquipmentStat(j);
            }
        }
    }
    
    /// <summary>
    /// TotalStat을 계산한다.
    /// </summary>
    private void AddEquipmentStat(int idx)
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            Stat[(EStatType)i] += _equipped[idx].FinalStat[(EStatType)i];
        }
        CurrentHealth += _equipped[idx].FinalStat[EStatType.Health];
    }
    
    private void SubtractEquipmentStat(int idx)
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            Stat[(EStatType)i] -= _equipped[idx].FinalStat[(EStatType)i];
        }
        CurrentHealth -= _equipped[idx].FinalStat[EStatType.Health];
    }
}