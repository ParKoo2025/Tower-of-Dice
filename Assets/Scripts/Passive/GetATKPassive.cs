using UnityEngine;

public class GetATKPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;

    public string Name { get; } = "GetATKPassive";
    public void Activate(Combatant owner)
    {
        _owner = owner;
        owner.AddStat(EStatType.ATK, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.ATK, -_amount);
    }
}