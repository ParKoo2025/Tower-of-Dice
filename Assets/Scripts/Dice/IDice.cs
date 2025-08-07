using UnityEngine;

public enum EDiceType
{
    Basic, High, Low, Odd, Even
}

public interface IDice
{
    public int RollTheDice(out Sprite diceSprite);
}

public class BasicDice : IDice
{
    private Sprite[] _sprites = Resources.LoadAll<Sprite>("Dice/Basic");    
    
    public int RollTheDice(out Sprite diceSprite)
    {
        int value = UnityEngine.Random.Range(0, 6);
        diceSprite = _sprites[value];
        return value + 1;
    }
}

public class HighDice : IDice
{
    private Sprite[] _sprites = Resources.LoadAll<Sprite>("Dice/High");    
    public int RollTheDice(out Sprite diceSprite)
    {
        int value = UnityEngine.Random.Range(0, 3);
        diceSprite = _sprites[value];
        return value + 4;
    }
}

public class LowDice : IDice
{
    private Sprite[] _sprites = Resources.LoadAll<Sprite>("Dice/Low");    
    public int RollTheDice(out Sprite diceSprite)
    {
        int value = UnityEngine.Random.Range(0, 3);
        diceSprite = _sprites[value];
        return value + 1;
    }
}

public class OddDice : IDice
{
    private Sprite[] _sprites = Resources.LoadAll<Sprite>("Dice/Odd");    

    public EDiceType DiceType { get; } = EDiceType.Odd;
    public int RollTheDice(out Sprite diceSprite)
    {
        int value = UnityEngine.Random.Range(0, 3);
        diceSprite = _sprites[value];
        return value * 2 + 1;
    }
}

public class EvenDice : IDice
{
    private Sprite[] _sprites = Resources.LoadAll<Sprite>("Dice/Even");    
    public int RollTheDice(out Sprite diceSprite)
    {
        int value = UnityEngine.Random.Range(0, 3);
        diceSprite = _sprites[value];
        return value * 2 + 2;
    }
}