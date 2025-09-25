using UnityEngine;

public class OnMonsterDeathGetHPPassive : IPassive
{
    private float _amount = 10;
    private Combatant _owner;
    
    public string Name { get; } = "OnMonsterDeathGetHPPassive";
    public void Activate(Combatant owner)
    {
        PassiveBus.Subscribe(EPassiveTriggerType.OnMonsterDeath, OnMonsterDeath);
        _owner = owner;
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnMonsterDeath, OnMonsterDeath);
    }

    private void OnMonsterDeath()
    {
        _owner.AddStat(EStatType.HP, _amount);
        Debug.Log($"{Name}");
    }
}
