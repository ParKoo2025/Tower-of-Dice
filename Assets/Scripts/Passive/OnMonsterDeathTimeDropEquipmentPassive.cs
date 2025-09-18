using UnityEngine;

public class OnMonsterDeathTimeDropEquipmentPassive : IPassive
{
    private float _time = 10f;
    
    public string Name { get; } = "OnMonsterDeathDropEquipmentPassive";
    public void Activate(Combatant owner)
    {
        PassiveBus.Subscribe(EPassiveTriggerType.OnMonsterDeath, onMonsterDeath);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnMonsterDeath, onMonsterDeath);
    }

    private void onMonsterDeath()
    {
        if (CombatManager.Instance.IsPastTime(_time))
        {
            GameManager.Instance.SpawnEquipment();
        }
    }
}