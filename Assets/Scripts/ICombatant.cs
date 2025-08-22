public interface ICombatant
{
    public bool IsDead { get; }
    public bool CanAttack { get; }
    public void Init();
    public bool TryStartAttack();
    public void OnAttackHit();
    public void OnAttackComplete();
    public void TakeDamage(float damage);
    public void StopAttack();
}