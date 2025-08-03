using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    private List<Tile> _tiles;

    private int _playerPosition;
    [SerializeField] private GameObject _player;

    public static int TreasureCount = 0;
    
    private void Start()
    {
        
        GenerateBoard();
        _playerPosition = 0;
        _player = Instantiate(_player, _tiles[_playerPosition].transform, false);
        _player.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        Camera.main.transform.SetParent(_player.transform);
    }

    private void Update()
    {
        int moveCount = 0;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            moveCount = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            moveCount = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            moveCount = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            moveCount = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            moveCount = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            moveCount = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            moveCount = 7;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            moveCount = 8;
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            moveCount = 9;
        else if (Input.GetKeyDown(KeyCode.A))
            moveCount = Random.Range(0, 12) + 1;
        
        if (moveCount > 0)
            ProcessMove(moveCount);
    }

    private void ProcessMove(int moveCount)
    {
        int prePosition = _playerPosition;
        _playerPosition = (_playerPosition + moveCount) % _tiles.Count;

        // 다음 위치로 이동
        var playerTile = _tiles[_playerPosition];
        _player.transform.SetParent(playerTile.transform, false);
        playerTile.Execute();

        var tileType = playerTile.ETileType;
        
        // 일단 LT, RT 타일은 Basic으로 갱신
        if (playerTile.ETileStyle != ETileStyle.C)
        {
            playerTile.ETileType = ETileType.Basic;
            playerTile.SetTileSprite();
        }

        // 밟은 타일이 Treasure Start인 경우
        if (tileType == ETileType.Treasure && TreasureCount == 1)
        {
            List<int> v = new List<int>();
            for (int i = 1; i < 40; i++)
            {
                if (i % 10 != 0)
                {
                    v.Add(i);
                }
            }

            var shuffled = v.OrderBy(_ => Random.value).ToList();

            foreach (var i in shuffled)
            {
                if (_tiles[i].ETileType == ETileType.Basic)
                {
                    _tiles[i].ETileType = ETileType.Treasure;
                    print("Treasure 도착 생성");
                    _tiles[i].SetTileSprite();
                    break;
                }
            }
        }
        
        // 시작 지점을 지나갈 때는 갱신을 해야 합니다.
        if (prePosition > _playerPosition)
        {
            foreach (var tile in _tiles)
            {
                tile.UpdateTile();
            }
        }
    }

    private void GenerateBoard()
    {
        _tiles = TileUtil.TileGenerate(transform);

        List<int> v = new List<int>();
        for (int i = 1; i < 40; i++)
        {
            if (i % 10 != 0)
            {
                v.Add(i);
            }
        }

        var shuffled = v.OrderBy(_ => Random.value).ToList();
        
        // 전투 타일
        for (int i = 0; i < 8; i++)
        {
            _tiles[shuffled[i]].ETileType = ETileType.Battle;
            _tiles[shuffled[i]].SetTileSprite();
        }
        
        // 이벤트 타일
        for (int i = 8; i < 29; i++)
        {
            int value = Random.Range(1, 5);

            _tiles[shuffled[i]].ETileType = (ETileType)value;
            _tiles[shuffled[i]].SetTileSprite();
        }
        
        // 보물 찾기 타일
        _tiles[shuffled[30]].ETileType = ETileType.Treasure;
        _tiles[shuffled[30]].SetTileSprite();
        TreasureCount = 1;
    }
    
}
