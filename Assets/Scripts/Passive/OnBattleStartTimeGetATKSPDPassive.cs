using System.Collections;
using UnityEngine;

public class OnBattleStartTimeGetATKSPDPassive : IPassive
{
    private float _time = 5f;
    private float _amount = 0.5f;
    private Combatant _owner;

    private Coroutine _coWait;
    
    public string Name { get; } = "OnBattleStartTimeGetATKSPDPassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;
        PassiveBus.Subscribe(EPassiveTriggerType.OnBattleStart, OnBattleStart);
        PassiveBus.Subscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);
    }

    public void Deactivate()
    {
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnBattleStart, OnBattleStart);
        PassiveBus.Unsubscribe(EPassiveTriggerType.OnBattleEnd, OnBattleEnd);
    }

    private void OnBattleStart()
    {
        _owner.AddStat(EStatType.ATK_SPD, _amount);
        _coWait = GameManager.Instance.StartCoroutine(CoWait());
    }

    private void OnBattleEnd()
    {
        if (_coWait != null)
        {
            GameManager.Instance.StopCoroutine(_coWait);
            DeleteAdvantage();
        }
    }

    private IEnumerator CoWait()
    {
        yield return new WaitForSeconds(_time);
        DeleteAdvantage();
    }

    private void DeleteAdvantage()
    {
        _owner.AddStat(EStatType.ATK_SPD, -_amount);
        _coWait = null;
        Debug.Log(Name);
    }
}
