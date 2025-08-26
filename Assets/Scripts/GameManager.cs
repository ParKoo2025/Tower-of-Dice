using System;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private EDiceType _leftDice = EDiceType.Basic;
    [SerializeField] private EDiceType _rightDice = EDiceType.Basic;
    
    [SerializeField] private Equipment[] _equipmentPrefab;

    public int CurFloor { get; set; } = 1;

    public enum EGameState
    {
        // 초기화 단계
        Init,
        
        // 주사위 굴리기를 기다리는 상태
        Idle,
        
        // 이동하는 상태
        Move,
        
        // 각 타일마다 이벤트를 진행하는 상태
        Event,
        
        // 타일 이벤트 마무리 상태
        EndEvent,
    }

    public EGameState GameState { get; set; } = EGameState.Init;

    public void OnBattleEnd(bool isPlayerWin)
    {
        GameState = EGameState.EndEvent;
        
        if (isPlayerWin)
            OnPlayerBattleWin();
        else
            OnPlayerBattleLose();

        StartCoroutine(WaitForCombatEnding());
    }
    
    private void Update()
    {
        if (GameState == EGameState.Idle)
            HandleInput();
    }

    private void HandleInput()
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
            moveCount = 10;
        // 장비 테스트
        else if (Input.GetKeyDown(KeyCode.W))
            SpawnEquipment(0);
        else if (Input.GetKeyDown(KeyCode.I))
            SpawnEquipment(1);
        
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Sprite leftDiceSprite = null;
            Sprite rightDiceSprite = null;
            int left = Dice.RollTheDice(_leftDice, out leftDiceSprite);
            int right = Dice.RollTheDice(_rightDice, out rightDiceSprite);

            // main UI에 sprite 전달해주기로 변경하기!
            //_leftDiceImage.sprite = leftDiceSprite;
            //_rightDiceImage.sprite = rightDiceSprite;
            
            print($"{_leftDice} -> {left} {_rightDice} -> {right}\n 이동 거리 : {left + right}");
            moveCount = left + right;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            moveCount = 39;
        }

        if (moveCount > 0)
        {
            GameState = EGameState.Move;
            TileManager.Instance.MovePlayer(moveCount);
        }
    }
    
    // 장비 테스트
    private void SpawnEquipment(int idx)
    {
        if (_equipmentPrefab == null || Camera.main == null) return;

        Vector3 spawnPos;
        
        var screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        spawnPos = Camera.main.ScreenToWorldPoint(screenCenter);
        spawnPos.z = -1f;
        

        var eq = Instantiate(_equipmentPrefab[idx], spawnPos, Quaternion.identity);
        eq.transform.localScale *= 2f;
    }
    
    private void OnPlayerBattleWin()
    {
        
        
    }

    private void OnPlayerBattleLose()
    {
        
    }

    private IEnumerator WaitForCombatEnding()
    {
        yield return new WaitForSeconds(2f);
        ProcessCombatEnding();
    }

    private void ProcessCombatEnding()
    {
        CombatManager.Instance.EndBattle();
        
        TileManager.Instance.ResetPlayerPosition();
        TileManager.Instance.ResetTile();

        GameState = EGameState.Idle;
    }
}
