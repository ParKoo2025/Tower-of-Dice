using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    private List<Tile> _tiles;

    private int _playerPosition;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _jumpHeight = 2.0f;
    
    [SerializeField] private EDiceType _leftDice = EDiceType.Basic;
    [SerializeField] private EDiceType _rightDice = EDiceType.Basic;
    
    public static int TreasureCount = 0;
    
    private bool _isPlayerMove = true;
    
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
        if (_isPlayerMove)
            return;
        
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
        {
            int left = Dice.RollTheDice(_leftDice);
            int right = Dice.RollTheDice(_rightDice);
            
            print($"{_leftDice} -> {left} {_rightDice} -> {right}\n 이동 거리 : {left + right}");
            moveCount = left + right;
        }

        if (moveCount > 0)
        {
            int nxt = (_playerPosition + moveCount) % _tiles.Count;
            StartCoroutine(MovePlayer(_playerPosition, nxt));
        }
    }


    private void GenerateBoard()
    {
        _tiles = TileUtil.GenerateTileObject(transform);
        var sequece = TileUtil.GenerateTileSpawnAnimation(_tiles).OnComplete(() => { _isPlayerMove = false; });

        List<int> v = new List<int>();
        for (int i = 1; i < 40; i++)
        {
            if (i % 10 != 0 && i != 8)
            {
                v.Add(i);
            }
        }

        var shuffled = v.OrderBy(_ => Random.value).ToList();
        
        // 전투 타일
        for (int i = 0; i < 22; i++)
        {
            _tiles[shuffled[i]].ChangeTile(ETileType.Battle);
        }
        
        // 이벤트 타일
        for (int i = 22; i < 28; i++)
        {
            int value = Random.Range(2, 5);

            _tiles[shuffled[i]].ChangeTile((ETileType)value);
        }
        
        // 보물 찾기 타일
        _tiles[shuffled[29]].ChangeTile(ETileType.Treasure);
        TreasureCount = 1;
        
        // 패시브 타일
        _tiles[8].ChangeTile(ETileType.Passive);
    }


    private IEnumerator MovePlayer(int start, int end)
    {
        _isPlayerMove = true;
        // 한 칸씩 이동합니다.
        for (int cur = start; cur != end; cur = (cur + 1) % _tiles.Count)
        {
            // 다음 타일 계산
            int nxt = (cur + 1) % _tiles.Count;

            var curTransform = _tiles[cur].gameObject.transform;
            var nxtTransform = _tiles[nxt].gameObject.transform;
            
            // 이동 방향 계산하여 캐릭터 방향 결정
            Vector3 delta = nxtTransform.position - curTransform.position;
            if (delta.x > 0) _player.transform.localScale = new Vector3(-5f, 5f, 5f);
            else _player.transform.localScale = new Vector3(5f, 5f, 5f);
    
            // 포물선 형태로 이동
            float elapsed = 0f;
            float duration = nxt == end ? 0.6f : 0.4f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;

                Vector3 pos = Vector3.Lerp(curTransform.position, nxtTransform.position, t);
                // 포물선 공식 -4h(t - 0.5)^2 + h
                float heightOffset = -4 * _jumpHeight * (t - 0.5f) * (t - 0.5f) + _jumpHeight;
                pos.y += heightOffset;

                _player.transform.position = pos;
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // 다음 위치로 이동
            _player.transform.SetParent(nxtTransform, false);
            _player.transform.localPosition = Vector3.zero;
            
            // 출발 지점을 지나갈 때는 전체 타일 업데이트
            // TODO
            // 코드 분리
            if (nxt == 0)
            {
                foreach (var tile in _tiles)
                {
                    tile.UpdateTile();
                }
            }
        }
        
        // 위치 갱신하고 Execute 처리
        _playerPosition = end;
        ExecuteTile(_tiles[_playerPosition]);
        _isPlayerMove = false;
    }
    
    private void ExecuteTile(Tile playerTile)
    {
        ETileType tileType = playerTile.ETileType;
        
        playerTile.Execute();
        
        // 밟은 타일이 Treasure Start인 경우 Basic 타일 중에서 Treasure End를 하나 생성합니다.
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
                    _tiles[i].ChangeTile(ETileType.Treasure);
                    print("Treasure 도착 생성");
                    break;
                }
            }
        }
    }
}
 

