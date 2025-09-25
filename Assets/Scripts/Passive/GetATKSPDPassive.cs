using UnityEngine;

public class GetATKSPDPassive : IPassive
{
    private float _amount = 0.5f;
    private Combatant _owner;

    public string Name { get; } = "GetATKSPDPassive";
    public void Activate(Combatant owner)
    {
        _owner = owner;
        owner.AddStat(EStatType.ATK_SPD, _amount);
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        _owner.AddStat(EStatType.ATK_SPD, -_amount);
    }
}
