using System;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private static Dictionary<EDiceType, IDice> _diceDictionary = new Dictionary<EDiceType, IDice>();

    public static int RollTheDice(EDiceType diceType, out Sprite diceSprite)
    {
        var dice = _diceDictionary[diceType];
        return dice.RollTheDice(out diceSprite);
    }

    private void Awake()
    {
        Initialize();
    }

    private static void Initialize()
    {
        _diceDictionary.Add(EDiceType.Basic, new BasicDice());
        _diceDictionary.Add(EDiceType.High, new HighDice());
        _diceDictionary.Add(EDiceType.Low, new LowDice());
        _diceDictionary.Add(EDiceType.Odd, new OddDice());
        _diceDictionary.Add(EDiceType.Even, new EvenDice());
    }
}
