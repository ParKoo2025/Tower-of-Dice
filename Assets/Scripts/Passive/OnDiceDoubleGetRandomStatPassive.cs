using UnityEngine;

public class OnDiceDoubleGetRandomStatPassive : IPassive
{
    private float _amount = 10;
    private Combatant _owner;
    public string Name { get; } = "OnDiceDoubleGetRandomStatPassive";

    public void Activate(Combatant owner)
    {
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnDiceDouble, onDiceDouble);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnDiceDouble, onDiceDouble);
    }

    private void onDiceDouble()
    {
        EStatType randStat = (EStatType)Random.Range(0, (int)EStatType.Size - 1);

        if (randStat != EStatType.ATK_SPD)
        {
            _owner.AddStat(randStat, _amount);
        }
        else
        {
            _owner.AddStat(randStat, _amount / 100);
        }
        
        Debug.Log($"{randStat} {_amount} 증가");
    }
}
