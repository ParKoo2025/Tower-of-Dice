using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battle2TileBehaviour : ITileBehaviour, IBattleTileBehaviour
{
    public int Difficulty { get; } = 2;
    
    public void Update(Tile tile)
    {
        int value = Random.Range(0, 10);
        if (value < 7)
        {
            tile.ChangeTile(ETileType.Battle3);
            Debug.Log($"{tile.name} 난이도 증가 : {Difficulty + 1}");
        }    
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Battle2 Tile 도착");
        tile.ChangeTile(ETileType.Basic);
    }

}