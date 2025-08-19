public interface ICombatant
{
    bool IsDead { get; }

    bool IsAttack();
    float GetDamage(out float aocDamage);
    void TakeDamage(float damage);
}