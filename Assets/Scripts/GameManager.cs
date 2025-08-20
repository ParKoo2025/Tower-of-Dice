using System;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private EDiceType _leftDice = EDiceType.Basic;
    [SerializeField] private EDiceType _rightDice = EDiceType.Basic;

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
    }

    public EGameState GameState { get; set; } = EGameState.Init;

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
            moveCount = 9;
        
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

        if (moveCount > 0)
        {
            GameState = EGameState.Move;
            TileManager.Instance.MovePlayer(moveCount);
        }
    }
}
