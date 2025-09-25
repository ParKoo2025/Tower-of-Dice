using UnityEngine;

public class GetHPPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetHPPassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        _owner.AddStat(EStatType.HP, _amount);
        Debug.Log(Name);

    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.HP, -_amount);
    }
}