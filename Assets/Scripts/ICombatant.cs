using System;

public interface ICombatant
{
    public bool IsDead { get; }
    public void StartAttack();
    public void OnAttackHit();
    public void TakeDamage(float damage);
    public void StopAttack();
}