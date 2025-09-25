using UnityEngine;

public class GetLifeStealRatePassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetLifeStealRatePassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        _owner.AddStat(EStatType.Lifesteal_Rate, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.Lifesteal_Rate, -_amount);
    }
}