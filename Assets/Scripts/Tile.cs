using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum eTileType
    {
        Basic, Ladder
    }

    public eTileType TileType { get; set; } = eTileType.Basic;

    public void SetSprite(eTileType tileType)
    {
        mSpriteRenderer.sprite = mTileSprites[(int)tileType];
        
        /* 애니메이션 주고 싶으면 여기에서 애니메이션 실행 시키면 되는 것 */
    }
    
    private void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private SpriteRenderer mSpriteRenderer;
    
    [SerializeField] 
    private List<Sprite> mTileSprites;
}
