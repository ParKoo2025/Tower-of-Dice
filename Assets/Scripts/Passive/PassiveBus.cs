using System;
using System.Collections.Generic;
using UnityEngine;

public enum EPassiveTriggerType
{
    OnMonsterDeath,
    OnPlayerDeath,
    OnDiceDouble,
    OnBattleStart,
    OnBattleEnd,
    OnPlayerAttack,
    OnStatChange,
}

public static class PassiveBus 
{
    private static readonly Dictionary<EPassiveTriggerType, List<Action>> _subscribers = new Dictionary<EPassiveTriggerType, List<Action>>();

    public static void Subscribe(EPassiveTriggerType type, Action action)
    {
        if (!_subscribers.ContainsKey(type))
        {
            _subscribers[type] = new List<Action>();
        }
        _subscribers[type].Add(action);
    }

    public static void Unsubscribe(EPassiveTriggerType type, Action action)
    {
        if (_subscribers.ContainsKey(type))
        {
            _subscribers[type].Remove(action);
        }
    }

    public static void Publish(EPassiveTriggerType OnDiceDoubleGetRandomStatPassive)
    {
        if (_subscribers.TryGetValue(OnDiceDoubleGetRandomStatPassive, out var subscribers))
        {
            foreach (var action in subscribers)
                action.Invoke();
        }
    }
}
