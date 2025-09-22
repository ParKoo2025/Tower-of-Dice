using UnityEditor;
using UnityEngine;

public class OnBattleEndRegenHPPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;
    
    public string Name { get; } = "OnBattleEndRegenHP";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);
    }

    private void OnBattleEnd()
    {
        _owner.RegenHP(_amount);
        Debug.Log(Name);
    }
}
