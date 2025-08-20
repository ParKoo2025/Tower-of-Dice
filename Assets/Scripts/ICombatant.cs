public interface ICombatant
{
    bool IsDead { get; }

    void Init();
    bool IsAttack();
    float GetDamage(out float aocDamage);
    void TakeDamage(float damage);
}