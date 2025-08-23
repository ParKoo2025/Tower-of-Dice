using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    [SerializeField] private List<Equipment> _equipment = new List<Equipment>();

    public Stat TotalStat { get; private set; } = new Stat();

    public void SetEquipment(Equipment eq)
    {
        //_equipmentStat[(int)equipmentType] = equipmentStat;
        CalculateStat();
    }    

    private void Start()
    {
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
               // sum += _equipment[j][(EStatType)i];
            }

            TotalStat[(EStatType)i] = sum;
        }
    }
}
