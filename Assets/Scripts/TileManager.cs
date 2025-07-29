using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tile = transform.GetChild(i).GetComponent<Tile>();
            mTiles.Add(tile);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            mTiles[1].SetSprite(Tile.eTileType.Basic);
        else if (Input.GetKeyDown(KeyCode.S))
            mTiles[1].SetSprite(Tile.eTileType.Ladder);
    }
    
    private List<Tile> mTiles = new List<Tile>();
}
