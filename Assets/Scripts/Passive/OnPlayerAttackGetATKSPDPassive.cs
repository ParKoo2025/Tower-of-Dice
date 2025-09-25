using UnityEngine;

public class OnPlayerAttackGetATKSPDPassive : IPassive
{
    private float _amount = 0.1f;
    private int _count = 0;
    private Combatant _owner;
    
    public string Name { get; } = "OnPlayerAttackGetATKSPDPassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnPlayerAttack, OnPlayerAttack);
        PassiveBus.Subscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);
        _count = 0;
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnPlayerAttack, OnPlayerAttack);
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);    
    }
    
    private void OnPlayerAttack()
    {
        _owner.AddStat(EStatType.ATK_SPD, _amount);
        _count++;
        Debug.Log($"{Name} {_count}");
    }

    private void OnBattleEnd()
    {
        // 실수 오차때문에 일단 이렇게 씁시다.
        for (int i = 0; i < _count; i++)
        {
            _owner.AddStat(EStatType.ATK_SPD, -_amount);
        }
        //_owner.AddStat(EStatType.ATK_SPD, -_amount * _count);
        _count = 0;
        Debug.Log($"{Name} Reset");
    }
}
