using System;
using UnityEngine;

public class OnPlayerDeathRevivePlayerPassive : IPassive
{
    private bool _isUse;
    private Combatant _owner;
    public string Name { get; } = "OnPlayerDeathRevivePlayer";
    
    public void Activate(Combatant owner)
    {
        _isUse = false;
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnPlayerDeath, OnPlayerDeath);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnPlayerDeath, OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        if (!_isUse)
        {
            _isUse = true;
            _owner.RegenHP(Single.MaxValue);
            _owner.ProcessAttack += CombatManager.Instance.ProcessPlayerAttack;
            _owner.StartAttack();
        }
    }
}
