using UnityEngine;

public class GetCounterAttackRatePassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetCounterAttackRatePassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        _owner.AddStat(EStatType.CounterAttackRate, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.CounterAttackRate, -_amount);
    }
}
