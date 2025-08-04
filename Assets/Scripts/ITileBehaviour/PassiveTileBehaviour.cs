using UnityEngine;

public class PassiveTileBehaviour : ITileBehaviour
{
    public void Update(Tile tile)
    {
        
    }

    public void Execute(Tile tile)
    {
        Debug.Log("Passive Tile 도착");
    }
}


