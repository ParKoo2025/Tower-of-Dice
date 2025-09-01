using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private Combatant _combatant;

    public void OnAttackHit()
    {
        _combatant.OnAttackHit();
    }
    
    private void Awake()
    {
        _combatant = GetComponentInParent<Combatant>();
    }
}
