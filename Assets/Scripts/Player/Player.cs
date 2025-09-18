using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Player : Combatant
{
    private List<IPassive> _passives;

    private void Start()
    {
        _passives = new List<IPassive>();
        var p = new OnMonsterDeathMaxHpIncreasePassive();
        _passives.Add(p);
        p.Activate(this);
    }
    
    public void SetEquipment(Equipment equipment)
    {
        _stat.SetEquipment(equipment);
        
        // 어라 체력이 0이네
        if (_stat.CurrentHealth <= 0)
        {
            IsDead = true;
            GameManager.Instance.OnBattleEnd(false);
        }
    }

    public void ReCalculateEquipmentStats()
    {
        _stat.ReCalculateEquipmentStats();
    }
}
    