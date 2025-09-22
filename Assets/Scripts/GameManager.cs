using System;
using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private EDiceType _leftDice = EDiceType.Basic;
    [SerializeField] private EDiceType _rightDice = EDiceType.Basic;
    
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
        PassiveBus.Publish(EPassiveTriggerType.OnBattleEnd);

        if (isPlayerWin)
            OnPlayerBattleWin();
        else
            OnPlayerBattleLose();

        StartCoroutine(WaitForCombatEnding());
    }
    
    // 장비 테스트
    public void SpawnEquipment()
    {
        Vector3 spawnPos;
        
        var screenCenter = TileManager.Instance.Player.transform.position;


        var eq = EquipmentManager.Instance.SpawnRandom();
        eq.transform.position = screenCenter;
        eq.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
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
            SpawnEquipment();
        
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Sprite leftDiceSprite = null;
            Sprite rightDiceSprite = null;
            int left = Dice.RollTheDice(_leftDice, out leftDiceSprite);
            int right = Dice.RollTheDice(_rightDice, out rightDiceSprite);

            if (left == right)
            {
                PassiveBus.Publish(EPassiveTriggerType.OnDiceDouble);
            }

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
