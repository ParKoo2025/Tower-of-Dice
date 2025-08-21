using UnityEngine;

public class TreasureTileBehaviour : ITileBehaviour
{
    private static bool _isTreasureStart = true;
    
    public void Update(Tile tile)
    {
        
    }

    public void Execute(Tile tile)
    {
        if (_isTreasureStart)
        {
            Debug.Log("Treasure Start 도착");
            _isTreasureStart = false;
        }
        else
        {
            Debug.Log("Treasure End 도착");
            _isTreasureStart = true;
            TileManager.TreasureCount = 0;
        }
        
        tile.ChangeTile(ETileType.Basic);
        
        GameManager.Instance.GameState = GameManager.EGameState.Idle;
    }
}