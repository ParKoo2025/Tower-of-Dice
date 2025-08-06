using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ETileType
{
    Basic,
    Passive, Roulette, Ladder, Toss, Treasure,
    Battle,
    Smithy, Airplane, Store, Bank, Start, Alchemy,
    Size
}

public enum ETileStyle
{
    C, LT, RT
}

public class TileUtil : MonoBehaviour
{
    [InfoBox("타일이 화면에 보이는 시간")]
    [ShowInInspector] private static float _fadeInAnimationTime = 0.1f;
    [InfoBox("타일 떨어지는 애니메이션 (google에 DOTween Ease 치면 곡선 종류 나옴)")]
    [ShowInInspector] private static Ease _tileAnimation = Ease.OutBounce;
    [InfoBox("타일이 떨어지는 시간")]
    [ShowInInspector] private static float _tileMoveAnimationTime = 0.3f;
    [InfoBox("타일이 떨어지는 간격")]
    [ShowInInspector] private static float _tileMoveAnimationIntervalTime = 0.1f;
     
    private static readonly string SPRITE_PATH = "ASE/";    

    private static Dictionary<ETileType, Sprite> _cTileSprites;
    private static Dictionary<ETileType, Sprite> _ltTileSprites;
    private static Dictionary<ETileType, Sprite> _rtTileSprites;

    public static Sprite GetTileSprite(ETileStyle eTileStyle, ETileType eTileType)
    {
        Sprite sprite = null;
        
        if (eTileStyle == ETileStyle.C)
        {
            _cTileSprites.TryGetValue(eTileType, out sprite);
        }
        else if (eTileStyle == ETileStyle.LT)
        {
            _ltTileSprites.TryGetValue(eTileType, out sprite);

        }
        else if (eTileStyle == ETileStyle.RT)
        {
            _rtTileSprites.TryGetValue(eTileType, out sprite);
        }

        return sprite;
    }

    public static List<Tile> TileGenerate(Transform parent)
    {
        var tiles = new List<Tile>();

        float delay = _tileMoveAnimationIntervalTime;
        
        
        
        // bottom C tile
        Vector3 spawnPosition = Vector3.zero;
        Tile tile = GenerateTile("Bottom C", spawnPosition, ETileStyle.C, parent);
        tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
        tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
        tiles.Add(tile);
        
        // Left RT tiles
        spawnPosition += new Vector3(-2.45f, 2.85f, 1.0f);
        Vector3 deltaPosition = new Vector3(-2.0f, 2.0f, 1.0f);
        for (int i = 0; i < 9; i++)
        {
            tile = GenerateTile("Left RT" + (i + 1), spawnPosition, ETileStyle.RT, parent);
            tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
            tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
            tiles.Add(tile);
            
            spawnPosition += deltaPosition;
        }

        // left c Tile
        spawnPosition += new Vector3(-0.85f, 0.45f, 1.0f);
        tile = GenerateTile("Bottom C", spawnPosition, ETileStyle.C, parent);
        tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
        tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
        tiles.Add(tile);
        
        // Left LT Tiles;
        spawnPosition += new Vector3(2.85f, 2.45f, 1.0f);
        deltaPosition = new Vector3(2.0f, 2.0f, 1.0f);
        for (int i = 0; i < 9; i++)
        {
            tile = GenerateTile("Left LT" + (i + 1), spawnPosition, ETileStyle.LT, parent);
            tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
            tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
            tiles.Add(tile);
            
            spawnPosition += deltaPosition;
        }
        
        // Top C tile
        spawnPosition += new Vector3(0.45f, 0.85f, 1.0f);
        tile = GenerateTile("Top C", spawnPosition, ETileStyle.C, parent);
        tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
        tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
        tiles.Add(tile);
        
        // Right RT tile
        spawnPosition += new Vector3(2.45f, -2.85f, -1.0f);
        deltaPosition = new Vector3(2.0f, -2.0f, -1.0f);
        for (int i = 0; i < 9; i++)
        {
            tile = GenerateTile("Right RT" + (i + 1), spawnPosition, ETileStyle.RT, parent);
            tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
            tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
            tiles.Add(tile);

            spawnPosition += deltaPosition;
        }
        
        // Right C Tile
        spawnPosition += new Vector3(0.85f, -0.45f, -1.0f);
        tile = GenerateTile("Right C", spawnPosition, ETileStyle.C, parent);
        tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
        tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
        tiles.Add(tile);
        
        // Right LT tiles
        spawnPosition += new Vector3(-2.85f, -2.45f, -1.0f);
        deltaPosition = new Vector3(-2.0f, -2.0f, -1.0f);
        for (int i = 0; i < 9; i++)
        {
            tile = GenerateTile("Right LT" + (i + 1), spawnPosition, ETileStyle.LT, parent);
            tile.GetComponent<SpriteRenderer>().DOFade(1f, _fadeInAnimationTime).SetDelay(delay * tiles.Count);
            tile.transform.DOMove(spawnPosition, _tileMoveAnimationTime).SetDelay(delay * tiles.Count + _fadeInAnimationTime).SetEase(_tileAnimation);
            tiles.Add(tile);
            
            spawnPosition += deltaPosition;
        }
        
        return tiles;
    }

    private void Awake()
    {
        _cTileSprites = LoadTileSprite(ETileStyle.C);
        _ltTileSprites = LoadTileSprite(ETileStyle.LT);
        _rtTileSprites = LoadTileSprite(ETileStyle.RT);
    }

    private static Dictionary<ETileType, Sprite> LoadTileSprite(ETileStyle eTileStyle)
    {
        Dictionary<ETileType, Sprite> dic = new Dictionary<ETileType, Sprite>();
        for (int i = 0; i < (int)ETileType.Size; i++)
        {
            string path = SPRITE_PATH + (ETileType)i + "_" + eTileStyle;
            Sprite sprite = Resources.Load<Sprite>(path);
            if (sprite)
            {
                dic.Add((ETileType)i, sprite);
                print(path);
            }
        }

        return dic;
    }

    private static Tile GenerateTile(string name, Vector3 spawnPosition, ETileStyle eTileStyle, Transform parent)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(SPRITE_PATH + "Tile"), parent);
        go.transform.position = spawnPosition;
        
        var tile = go.GetComponent<Tile>();
        tile.InitializeTile(eTileStyle);
        
        tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        tile.transform.position = spawnPosition + Vector3.up * 5f;
    
        return tile;
    }
}

