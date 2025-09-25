using UnityEngine;

public class GetDEFPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetDEFPassive";
    public void Activate(Combatant owner)
    {
        _owner = owner;
        owner.AddStat(EStatType.DEF, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.DEF, -_amount);
    }
}