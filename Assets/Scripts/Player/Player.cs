using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Player : Combatant
{
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
    