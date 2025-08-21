using UnityEngine;

public class RouletteTileBehaviour : ITileBehaviour
{
    public void Update(Tile tile)
    {
        
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Roulette Tile 도착");
        tile.ChangeTile(ETileType.Basic);
        GameManager.Instance.GameState = GameManager.EGameState.Idle;
        
    }
}


