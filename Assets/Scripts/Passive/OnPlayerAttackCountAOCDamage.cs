using UnityEngine;

public class OnPlayerAttackCountAOCDamage : IPassive
{
    private int _count;
    private int _attackCount = 5;
    private float _damage = 10f;
    private Combatant _owner;

    public string Name { get; } = "OnPlayerAttackCountAOCDamage";

    public void Activate(Combatant owner)
    {
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnBattleStart, OnBattleStart);
        PassiveBus.Subscribe(EPassiveTriggerType.OnPlayerAttack, OnPlayerAttack);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnBattleStart, OnBattleStart);
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnPlayerAttack, OnPlayerAttack);
    }

    private void OnBattleStart()
    {
        _count = 0;
    }

    private void OnPlayerAttack()
    {
        if (++_count == _attackCount)
        {
            CombatManager.Instance.ProcessPlayerAttack(EDamageType.Attack, 0, _damage);
            _count = 0;
            Debug.Log(Name);
        }
    }

}
