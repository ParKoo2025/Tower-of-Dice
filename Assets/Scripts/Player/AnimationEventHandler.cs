using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private ICombatant _combatant;

    public void OnAttackHit()
    {
        _combatant.OnAttackHit();
    }

    public void OnAttackComplete()
    {
        _combatant.OnAttackComplete();
    }
    
    private void Awake()
    {
        _combatant = GetComponentInParent<ICombatant>();
    }
}
