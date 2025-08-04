using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleTileBehaviour : ITileBehaviour
{
    private int _difficulty = 1;
    
    public void Update(Tile tile)
    {
        if (_difficulty < 4)
        {
            int value = Random.Range(0, 10);
            if (value < 7)
            {
                _difficulty++;
                Debug.Log($"{tile.name} 난이도 증가 : {_difficulty}");
            }    
        }
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Battle Tile 도착");
        tile.ChangeTile(ETileType.Basic);
    }
}