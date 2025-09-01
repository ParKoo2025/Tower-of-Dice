using System.Collections.Generic;
using UnityEngine;

public class CombatantStat : MonoBehaviour
{
    [SerializeField] private StatScriptable _basicStat;
    [SerializeField] private List<Equipment> _equipment = new List<Equipment>();

    public Stat Stat { get; } = new Stat();
    public float CurrentHealth { get; set; }

    public void SetEquipment(Equipment equipment)
    {
        CalculateStat();
    }

    private void Awake()
    {
        CalculateStat();
        CurrentHealth = Stat[EStatType.Health];
    }

    private void CalculateStat()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            Stat[(EStatType)i] = _basicStat[(EStatType)i];
        }
    }
}