using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battle4TileBehaviour : ITileBehaviour, IBattleTileBehaviour
{
    private List<GameObject> _monsters;

    public int Difficulty { get; } = 4;
    
    public Battle4TileBehaviour()
    {
        _monsters = new List<GameObject>();
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
        _monsters.Add(MonsterFactory.Instance.GetRandomMonster());
    }
    
    public void Update(Tile tile)
    {

    }

    public void Execute(Tile tile)
    {
        CombatManager.Instance.StartBattle(_monsters);        

    }

}