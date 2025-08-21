using UnityEngine;

public class BasicTileBehaviour : ITileBehaviour
{
    public void Update(Tile tile)
    {
        int value = Random.Range(0, 3);

        if (value == 0)
        {
            value = Random.Range(2, 6 - TileManager.TreasureCount);
            ETileType eTileType = (ETileType)value; 
            tile.ChangeTile(eTileType);
            
            if (eTileType == ETileType.Treasure)
            {
                TileManager.TreasureCount = 1;
            }
        }
        else if (value == 1)
        {
            tile.ChangeTile(ETileType.Battle1);
        }
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Basic Tile 도착");
        tile.ChangeTile(ETileType.Basic);
        GameManager.Instance.GameState = GameManager.EGameState.Idle;

    }
}
