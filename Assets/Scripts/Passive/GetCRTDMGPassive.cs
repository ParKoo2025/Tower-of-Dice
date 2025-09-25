using UnityEngine;

public class GetCRTDMGPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetCRTDMGPassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        _owner.AddStat(EStatType.CRT_DMG, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.CRT_DMG, -_amount);
    }
}