using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    
    private SpriteRenderer _spriteRenderer;
    private int _difficulty = 0;
    private static bool _treasureStart = true;

    [SerializeField] [ReadOnly]
    private ETileStyle _eTileStyle;


    public ETileStyle ETileStyle
    {
        get => _eTileStyle;
        set => _eTileStyle = value;
    }
    
    [SerializeField] [ReadOnly]    
    private ETileType _eTileType;

    public ETileType ETileType
    {
        get => _eTileType;
        set
        {
            if (value == ETileType.Battle)
            {
                _difficulty = 1;
            }
            _eTileType = value;   
        }
    }

    public void SetTileSprite()
    {
        var sprite = TileUtil.GetTileSprite(ETileStyle, ETileType);
        if (!sprite)
        {
            sprite = TileUtil.GetTileSprite(ETileStyle, ETileType.Basic);
        }
        _spriteRenderer.sprite = sprite;
    }

    public void UpdateTile()
    {
        if (ETileStyle == ETileStyle.C)
            return;

        if (ETileType == ETileType.Basic)
        {
            UpdateTileBasic();   
        }
        else if (ETileType == ETileType.Battle)
        {
            UpdateTileBattle();
        }
    }

    public void Execute()
    {
        if (ETileStyle == ETileStyle.C)
        {
            ExecuteC();
        }
        else
        {
            ExecuteT();
        }
    }
    
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateTileBasic()
    {
        int value = Random.Range(0, 3);

        if (value == 0)
        {
            value = Random.Range(1, 6 - TileManager.TreasureCount);
            ETileType = (ETileType)value;
            SetTileSprite();
            print(name + $"{ETileType}로 전환");

            if (ETileType == ETileType.Treasure)
            {
                TileManager.TreasureCount = 1;
            }
        }
        else if (value == 1)
        {
            ETileType = ETileType.Battle;
            SetTileSprite();
            print(name + $"{ETileType}로 전환");
        }
    }

    private void UpdateTileBattle()
    {
        int value = Random.Range(0, 10);
        if (value < 7)
        {
            _difficulty++;
            print(name + $"난이도 증가 {_difficulty}");
        }
    }

    private void ExecuteC()
    {
        
    }

    private void ExecuteT()
    {
        switch (ETileType)
        {
            case ETileType.Passive:
                print("Passive 도착");
                break;
            case ETileType.Roulette:
                print("Roulette 도착");
                break;
            case ETileType.Ladder:
                print("Ladder 도착");
                break;
            case ETileType.Toss:
                print("Coin Toss 도착");
                break;
            case ETileType.Treasure:
                ExecuteTreasure();
                break;
            case ETileType.Battle:
                print("전투 도착");
                break;
        }
    }

    private void ExecuteTreasure()
    {
        if (_treasureStart)
        {
            print("TreasureStart 도착!!!");
            _treasureStart = false;
        }
        else
        {
            print("보물을 찾았다!");
            _treasureStart = true;
            TileManager.TreasureCount = 0;
        }
    }

}
