using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Statistics", menuName = "ScriptableObjects/Statistics", order = int.MaxValue)]
public class StatScriptable : ScriptableObject
{
    [SerializeField]
    [ShowInInspector] // 이 줄이 중요!
    private Stat _stats = new Stat();
 
    public float this[EStatType type]   
    {
        get => _stats[type];
        set => _stats[type] = value;
    }
}