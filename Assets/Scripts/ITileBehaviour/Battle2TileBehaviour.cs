using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battle2TileBehaviour : ITileBehaviour, IBattleTileBehaviour
{
    private List<GameObject> _monsters;

    public int Difficulty { get; } = 2;
    
    public Battle2TileBehaviour()
    {
        _monsters = new List<GameObject>();
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
    }
    
    public void Update(Tile tile)
    {
        int value = Random.Range(0, 10);
        if (value < 7)
        {
            tile.ChangeTile(ETileType.Battle3);
            Debug.Log($"{tile.name} 난이도 증가 : {Difficulty + 1}");
        }    
    }

    public void Execute(Tile tile)
    {
        CombatManager.Instance.StartBattle(_monsters);        

    }

}