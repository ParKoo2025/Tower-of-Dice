using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ICombatant
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private HPController _hpController;
    private PlayerStat _playerStat;
    
    private float _attackCooldownTimer = 0.0f;
    private float _healthRegenCooldownTimer = 0.0f;
    private bool _isAttacking = false;

    private float _pendingDamage = 0.0f;
    private float _pendingAocDamage = 0.0f;

    private bool _progressCombat;
    
    public bool IsDead { get; private set; }
    public bool CanAttack => !_isAttacking && _attackCooldownTimer <= 0f && !IsDead;

    public void Init()
    {
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];
        _healthRegenCooldownTimer = 1.0f;
        _isAttacking = false;
        IsDead = false;

        _hpController.SetHealth(_playerStat.CurrentHealth, _playerStat.TotalStat[EStatType.Health]);
        _hpController.SetAttackSpeed(Mathf.Max(0f, _attackCooldownTimer), _playerStat.TotalStat[EStatType.AttackSpeed]);
        _hpController.gameObject.SetActive(true);
        _progressCombat = true;
    }

    public bool TryStartAttack()
    {
        if (!CanAttack || IsDead) return false;

        _isAttacking = true;
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];

        _pendingDamage = CalculateAttackDamage();
        _pendingAocDamage = _playerStat.TotalStat[EStatType.AocDamage];
        
        //_playerAnimator.Play("ATTACK");
        _playerAnimator.SetTrigger("2_Attack");
        return true;
    }

    public void OnAttackHit()
    {
        if (IsDead) return;
        
        print("player OnAttackHit");
        int aocCount = CombatManager.Instance.ProcessPlayerAttack(_pendingDamage, _pendingAocDamage);

        // TODO
        // 체력 회복 효과
        float healthSteal = (_pendingDamage + _pendingAocDamage * aocCount) *
                            (_playerStat.TotalStat[EStatType.HealthStealRate] / 100f);
        ChangeHealth(healthSteal);
    }

    public void OnAttackComplete()
    {
        print("player OnAttackComplete");
        _isAttacking = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        bool isCounterAttack = false;
        damage = CalculateDefenseDamage(damage, out isCounterAttack);

        if (damage > 0f)
        {
            ChangeHealth(-damage);
        }

        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_playerStat.CurrentHealth})");

        if (_playerStat.CurrentHealth <= 0.0f)  
        {
            IsDead = true;
            _isAttacking = false;
            // 죽는 애니메이션 트리거 설정
            _playerAnimator.Play("DEATH");
            
            // 다른 애니메이션 파라미터들을 리셋하여 죽는 애니메이션이 방해받지 않도록 함
            _playerAnimator.ResetTrigger("2_Attack");

        }

        // 반격이 이게 맞나?
        if (isCounterAttack)
        {
            _attackCooldownTimer = 0f;
        } 
    }

    public void StopAttack()
    {
        _progressCombat = false;
        if (IsDead) return;

        _isAttacking = false;
        _hpController.gameObject.SetActive(false);
        //_playerAnimator.Play("IDLE");
    }

    private void Awake()
    {
        _playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        if (IsDead) return;
        if (!_progressCombat) return;
        
        if (_attackCooldownTimer > 0.0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
            _hpController.SetAttackSpeed(Mathf.Max(0f, _attackCooldownTimer), _playerStat.TotalStat[EStatType.AttackSpeed]);
        }

        _healthRegenCooldownTimer -= Time.deltaTime;
        if (_healthRegenCooldownTimer <= 0.0f)
        {
            // TODO
            // 체력 회복 효과 넣기
            _healthRegenCooldownTimer = 1f;
            ChangeHealth(_playerStat.TotalStat[EStatType.HealthRegen]);
        }
    }

    private void ChangeHealth(float delta)
    {
        _playerStat.CurrentHealth =
            Mathf.Clamp(_playerStat.CurrentHealth + delta, 0f, _playerStat.TotalStat[EStatType.Health]);
        _hpController.SetHealth(_playerStat.CurrentHealth, _playerStat.TotalStat[EStatType.Health]);
    }

    private float CalculateAttackDamage()
    {
        float damage = _playerStat.TotalStat[EStatType.AttackDamage];
        float rand = Random.Range(0, 99);
        if (_playerStat.TotalStat[EStatType.CriticalRate] > rand)
        {
            print("크리티컬");
            damage = damage * (1.5f + _playerStat.TotalStat[EStatType.CriticalMultiplier] / 100f);
        }

        return damage;
    }

    private float CalculateDefenseDamage(float damage, out bool isCounterAttack)
    {
        isCounterAttack = false;
        float rand = Random.Range(0, 99);
        if (_playerStat.TotalStat[EStatType.EvasionRate] > rand)
        {
            print("회피");
            return 0f;
        }

        damage = MathF.Max(0f, damage - _playerStat.TotalStat[EStatType.Defence]);
        rand = Random.Range(0, 99);
        if (_playerStat.TotalStat[EStatType.CounterAttackRate] > rand)
        {
            print("반격");
            isCounterAttack = true;
        }

        return damage;
    }
}
    