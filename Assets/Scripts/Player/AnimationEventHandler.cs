using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private ICombatant _combatant;

    public void OnAttackHit()
    {
        _combatant.OnAttackHit();
    }
    
    private void Awake()
    {
        _combatant = GetComponentInParent<ICombatant>();
    }
}
