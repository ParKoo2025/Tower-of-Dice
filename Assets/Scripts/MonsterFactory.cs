using UnityEngine;

public class MonsterFactory : SingletonBehavior<MonsterFactory>
{
    [SerializeField] private GameObject _monster;

    public GameObject GetRandomMonster()
    {
        return _monster;
    }
}
