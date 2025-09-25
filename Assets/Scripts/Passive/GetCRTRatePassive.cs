using UnityEngine;

public class GetCRTRatePassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetCRTRatePassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        _owner.AddStat(EStatType.CRT_Rate, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.CRT_Rate, -_amount);
    }
}