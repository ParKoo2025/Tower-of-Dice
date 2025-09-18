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
        var hpIncrease = new OnMonsterDeathMaxHpIncreasePassive();
        _passives.Add(hpIncrease);
        hpIncrease.Activate(this);

        var tDropEquipment = new OnMonsterDeathTimeDropEquipmentPassive();
        _passives.Add(tDropEquipment);
        tDropEquipment.Activate(this);

        var cDropEquipment = new OnMonsterDeathCountDropEquipmentPassive();
        _passives.Add(cDropEquipment);
        cDropEquipment.Activate(this);
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
    