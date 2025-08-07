using UnityEngine;

public enum EDiceType
{
    Basic, High, Low, Odd, Even
}

public interface IDice
{
    public EDiceType DiceType { get; }

    public int RollTheDice();
}

public class BasicDice : IDice
{
    public EDiceType DiceType { get; } = EDiceType.Basic;
    public int RollTheDice()
    {
        return UnityEngine.Random.Range(0, 6) + 1;
    }
}

public class HighDice : IDice
{
    public EDiceType DiceType { get; } = EDiceType.High;
    public int RollTheDice()
    {
        return UnityEngine.Random.Range(0, 3) + 4;
    }
}

public class LowDice : IDice
{
    public EDiceType DiceType { get; } = EDiceType.Low;
    public int RollTheDice()
    {
        return UnityEngine.Random.Range(0, 3) + 1;
    }
}

public class OddDice : IDice
{
    public EDiceType DiceType { get; } = EDiceType.Odd;
    public int RollTheDice()
    {
        return UnityEngine.Random.Range(0, 3) * 2 + 1;
    }
}

public class EvenDice : IDice
{
    public EDiceType DiceType { get; } = EDiceType.Even;
    public int RollTheDice()
    {
        return UnityEngine.Random.Range(0, 3) * 2 + 2;
    }
}