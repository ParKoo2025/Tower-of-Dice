using UnityEngine;

public class LadderTileBehaviour : ITileBehaviour
{
    public void Update(Tile tile)
    {
        
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Ladder Tile 도착");
        tile.ChangeTile(ETileType.Basic);
    }
}


