using UnityEngine;

public class TossTileBehaviour : ITileBehaviour
{
    public void Update(Tile tile)
    {
        
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Coin Toss Tile 도착");
        tile.ChangeTile(ETileType.Basic);
        
        GameManager.Instance.GameState = GameManager.EGameState.Idle;
    }
}
