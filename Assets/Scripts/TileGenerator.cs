using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private static readonly string BASIC_C_PATH = "GameObject/Basic_C";
    private static readonly string BASIC_LT_PATH = "GameObject/Basic_LT";
    private static readonly string BASIC_RT_PATH = "GameObject/Basic_RT";
    
    public void Generate()
    {
        mTiles = new List<Tile>();
        
        // bottom C tile
        Vector3 spawnPosition = Vector3.zero;
        GameObject go = Instantiate(Resources.Load<GameObject>(BASIC_C_PATH), spawnPosition, Quaternion.identity);
        mTiles.Add(go.GetComponent<Tile>());
        go.transform.parent = transform;
        
        // Left RT tiles
        spawnPosition += new Vector3(-2.5f, 2.9f, 0.0f);
        Vector3 deltaPosition = new Vector3(-2.0f, 2.0f, 1.0f);
        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(Resources.Load<GameObject>(BASIC_RT_PATH), spawnPosition, Quaternion.identity);
            mTiles.Add(go.GetComponent<Tile>());
            go.transform.parent = transform;
            spawnPosition += deltaPosition;
        }

        // left c Tile
        spawnPosition += new Vector3(-0.5f, 0.9f, 1.0f);
        go = Instantiate(Resources.Load<GameObject>(BASIC_C_PATH), spawnPosition, Quaternion.identity);
        mTiles.Add(go.GetComponent<Tile>());
        go.transform.parent = transform;

        // Left LT Tiles;
        spawnPosition += new Vector3(2.5f, 2.9f, 1.0f);
        deltaPosition = new Vector3(2.0f, 2.0f, 1.0f);

        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(Resources.Load<GameObject>(BASIC_LT_PATH), spawnPosition, Quaternion.identity);
            mTiles.Add(go.GetComponent<Tile>());
            go.transform.parent = transform;
            spawnPosition += deltaPosition;
        }
        
        // Right LT tiles
        spawnPosition = Vector3.zero;
        spawnPosition += new Vector3(2.5f, 2.9f, 0.0f);
        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(Resources.Load<GameObject>(BASIC_LT_PATH), spawnPosition, Quaternion.identity);
            mTiles.Add(go.GetComponent<Tile>());
            go.transform.parent = transform;
            spawnPosition += deltaPosition;
        }

        // Right C Tile
        spawnPosition += new Vector3(0.5f, 0.9f, 1.0f);
        go = Instantiate(Resources.Load<GameObject>(BASIC_C_PATH), spawnPosition, Quaternion.identity);
        mTiles.Add(go.GetComponent<Tile>());
        go.transform.parent = transform;

        // Right RT tile
        spawnPosition += new Vector3(-2.5f, 2.9f, 1.0f);
        deltaPosition = new Vector3(-2.0f, 2.0f, 1.0f);
        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(Resources.Load<GameObject>(BASIC_RT_PATH), spawnPosition, Quaternion.identity);
            mTiles.Add(go.GetComponent<Tile>());
            go.transform.parent = transform;
            spawnPosition += deltaPosition;
        }

        // Top C tile
        spawnPosition += new Vector3(-0.5f, 0.9f, 1.0f);
        go = Instantiate(Resources.Load<GameObject>(BASIC_C_PATH), spawnPosition, Quaternion.identity);
        mTiles.Add(go.GetComponent<Tile>());
        go.transform.parent = transform;
    }

    private List<Tile> mTiles;
}

