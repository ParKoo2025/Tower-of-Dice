using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combatant : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected HPController _hpController;
    protected CombatantStat _stat;
    
    private float _healthRegenCooldownTimer = 0.0f;
    private bool _isAttacking;
    
    public bool IsDead { get; protected set; }

    public event Action<EDamageType, float, float> ProcessAttack;
    
    public void StartAttack()
    {
        _healthRegenCooldownTimer = 1.0f;
        IsDead = false;

        _hpController.SetHealth(_stat.CurrentHealth, _stat.Stat[EStatType.Health]);
        _hpController.gameObject.SetActive(true);
        _isAttacking = true;

        _animator.speed = 1f / _stat.Stat[EStatType.AttackSpeed] / 2f;
        _hpController.SetAttackSpeed(0f, 1f);

        _animator.Play("WAIT");
    }

    public void OnAttackHit()
    {
        if (IsDead) return;

        bool isCritical;
        float damage = CalculateAttackDamage(out isCritical);
        float aocDamage = _stat.Stat[EStatType.AocDamage];

        EDamageType damageType = EDamageType.Attack;
        if (isCritical)
            damageType = EDamageType.CriticalAttack;

        ProcessAttack?.Invoke(damageType, damage, aocDamage);
        
        int aocCount = CombatManager.Instance.MonsterCount;
        
        float healthSteal = (damage + aocDamage * aocCount) * (_stat.Stat[EStatType.HealthStealRate] / 100f);
        ChangeHealth(healthSteal);
    }

    public void TakeDamage(EDamageType damageType, float damage)
    {
        if (IsDead) return;

        bool canCounterAttack = false;
        damage = CalculateDefenseDamage(damage, out canCounterAttack);
        
        CombatManager.Instance.OnChangeHealth?.Invoke(transform, damageType, damage);
        
        if (damage > 0f)
        {
            ChangeHealth(-damage);
        }

        if (_stat.CurrentHealth <= 0.0f)
        {
            IsDead = true;
            _isAttacking = false;
            ProcessAttack = null;

            _animator.speed = 1f;
            
            _animator.Play("DEATH");
            
            _animator.ResetTrigger("2_Attack");
        }

        if (canCounterAttack)
        {
            
        }
    }

    public void StopAttack()
    {
        if (IsDead) return;

        _isAttacking = false;
        ProcessAttack = null;
        
        _animator.speed = 1f;
        
        _hpController.gameObject.SetActive(false);
        _animator.Play("IDLE");
    }

    private void Awake()
    {
        _stat = GetComponent<CombatantStat>();
    }

    private void Update()
    {
        if (!_isAttacking) return;
        if (_stat.Stat[EStatType.HealthRegen] == 0f) return;
        
        _healthRegenCooldownTimer -= Time.deltaTime;
        if (_healthRegenCooldownTimer <= 0.0f)
        {
            _healthRegenCooldownTimer = 1.0f;
            
            
            ChangeHealth(_stat.Stat[EStatType.HealthRegen]);
            CombatManager.Instance.OnChangeHealth?.Invoke(transform, EDamageType.Heal, _stat.Stat[EStatType.HealthRegen]);
        }
    }

    private void LateUpdate()
    {
        if (!_isAttacking) return;
        
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _hpController.SetAttackSpeed(stateInfo.normalizedTime % 1.0f, 1f);

        _animator.speed = 1f / _stat.Stat[EStatType.AttackSpeed] / 2f;
    }

    private void ChangeHealth(float delta)
    {
        _stat.CurrentHealth =
            Mathf.Clamp(_stat.CurrentHealth + delta, 0f, _stat.Stat[EStatType.Health]);
        _hpController.SetHealth(_stat.CurrentHealth, _stat.Stat[EStatType.Health]);
    }

    
    private float CalculateAttackDamage(out bool isCritical)
    {
        isCritical = false; 
        float damage = _stat.Stat[EStatType.AttackDamage];
        float rand = Random.Range(0, 99);
        if (_stat.Stat[EStatType.CriticalRate] > rand)
        {
            isCritical = true;
            damage = damage * (1.5f + _stat.Stat[EStatType.CriticalMultiplier] / 100f);
        }

        return damage;
    }
    
    private float CalculateDefenseDamage(float damage, out bool canCounterAttack)
    {
        canCounterAttack = false;
        float rand = Random.Range(0, 99);
        if (_stat.Stat[EStatType.EvasionRate] > rand)
        {
            print("회피");
            return 0f;
        }

        damage = MathF.Max(0f, damage - _stat.Stat[EStatType.Defence]);
        rand = Random.Range(0, 99);
        if (_stat.Stat[EStatType.CounterAttackRate] > rand)
        {
            print("반격");
            canCounterAttack = true;
        }

        return damage;
    }
}