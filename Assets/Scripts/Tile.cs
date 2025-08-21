using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private ITileBehaviour _tileBehaviour;
    
    [SerializeField] [ReadOnly]
    private ETileStyle _eTileStyle;
    public ETileStyle ETileStyle => _eTileStyle;
    
    [SerializeField] [ReadOnly]    
    private ETileType _eTileType;
    public ETileType ETileType => _eTileType;

    public void InitializeTile(ETileStyle eTileStyle)
    {
        _eTileStyle = eTileStyle;
        _eTileType = ETileType.Basic;
        _tileBehaviour = TileBehaviourFactory.Create(ETileStyle, ETileType);
        SetTileSprite();
    }
    
    public void UpdateTile()
    {
        if (ETileStyle == ETileStyle.C)
            return;

        _tileBehaviour.Update(this);
    }

    public void Execute()
    {
        if (ETileStyle == ETileStyle.C)
            return;
        
        _tileBehaviour.Execute(this);
    }
    
    public void ChangeTile(ETileType eTileType)
    {
        Debug.Log($"{name} 타일 전환 {_eTileType} -> {eTileType}");
        _eTileType = eTileType;
        _tileBehaviour = TileBehaviourFactory.Create(ETileStyle, ETileType);
        SetTileSprite();
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void SetTileSprite()
    {
        var sprite = TileUtil.GetTileSprite(ETileStyle, ETileType);
        if (!sprite)
        {
            sprite = TileUtil.GetTileSprite(ETileStyle, ETileType.Basic);
        }
        _spriteRenderer.sprite = sprite;
    }
}
