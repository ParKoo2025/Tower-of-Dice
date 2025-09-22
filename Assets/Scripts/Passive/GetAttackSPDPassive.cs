using UnityEngine;

public class GetAttackSPDPassive : IPassive
{
    private float _amount = 0.5f;
    private Combatant _owner;

    public string Name { get; } = "GetAttackSPDPassive";
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
