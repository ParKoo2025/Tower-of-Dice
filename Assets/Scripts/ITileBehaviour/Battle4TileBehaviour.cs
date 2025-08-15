using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battle4TileBehaviour : ITileBehaviour, IBattleTileBehaviour
{
    public int Difficulty { get; } = 4;
    
    public void Update(Tile tile)
    {

    }

    public void Execute(Tile tile)
    {
        Debug.Log("Battle4 Tile 도착");
        tile.ChangeTile(ETileType.Basic);
    }

}