using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private void Start()
    {
        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            var tile = transform.GetChild(i).GetComponent<Tile>();
            mTiles.Add(tile);
        }*/

        mTileGenerator = GetComponent<TileGenerator>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
           mTileGenerator.Generate();
    }
    
    private List<Tile> mTiles = new List<Tile>();
    private TileGenerator mTileGenerator;
}
