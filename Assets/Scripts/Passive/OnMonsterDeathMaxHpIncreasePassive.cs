using UnityEngine;

public class OnMonsterDeathMaxHpIncreasePassive : IPassive
{
    private float _amount = 10;
    private Combatant _owner;
    
    public string Name { get; } = "OnMonsterDeathMaxHpIncreasePassive";
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
