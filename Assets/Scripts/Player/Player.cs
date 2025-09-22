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
        RegisterPassive();   
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
    
    public void SetHpControllerActive(bool isActive) 
    {
        _hpController.gameObject.SetActive(isActive);
    }

    private void RegisterPassive()
    {
        // var hpIncrease = new OnMonsterDeathGetMaxHpPassive();
        // _passives.Add(hpIncrease);
        // hpIncrease.Activate(this);
        
        // var doubleDice = new OnDiceDoubleGetRandomStatPassive();
        // _passives.Add(doubleDice);
        // doubleDice.Activate(this);
        
        // var tDropEquipment = new OnMonsterDeathTimeDropEquipmentPassive();
        // _passives.Add(tDropEquipment);
        // tDropEquipment.Activate(this);

        var revive = new OnPlayerDeathRevivePlayerPassive();
        _passives.Add(revive);
        revive.Activate(this);

        var regenHp = new OnBattleEndRegenHPPassive();
        _passives.Add(regenHp);
        regenHp.Activate(this);

        var getAtkSpd = new GetAttackSPDPassive();
        _passives.Add(getAtkSpd);
        getAtkSpd.Activate(this);

        var tGetAtkSpd = new OnBattleStartTimeGetAttackSPDPassive();
        _passives.Add(tGetAtkSpd);
        tGetAtkSpd.Activate(this);

        // var cDropEquipment = new OnMonsterDeathCountDropEquipmentPassive();
        // _passives.Add(cDropEquipment);
        // cDropEquipment.Activate(this);

    }
}
    