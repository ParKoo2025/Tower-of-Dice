using UnityEngine;

public class OnMonsterDeathCountDropEquipmentPassive : IPassive
{
    private int _count = 10;
    private int _deathCount = 0;
    public string Name { get; } = "OnMonsterDeathCountDropEquipmentPassive";
    
    public void Activate(Combatant owner)
    {
        PassiveBus.Subscribe(EPassiveTriggerType.OnMonsterDeath, onMonsterDeath);
        _deathCount = 0;
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    private void onMonsterDeath()
    {
        if (++_deathCount == _count)
        {
            GameManager.Instance.SpawnEquipment();
            _deathCount = 0;
            Debug.Log(Name);
        }
    }
}
