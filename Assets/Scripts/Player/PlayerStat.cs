using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    private List<StatScriptable> _equipmentStat;

    private void Start()
    {
        _equipmentStat = new List<StatScriptable>(4);
        
    }

    public Stat TotalStat { get; private set; }

    public void SetEquipment(EEquipmentType equipmentType, StatScriptable equipmentStat)
    {
        _equipmentStat[(int)equipmentType] = equipmentStat;
    }    
    
    /// <summary>
    /// TotalStat을 계산한다.
    /// </summary>
    private void CalculateStat()
    {
        
    }
}
