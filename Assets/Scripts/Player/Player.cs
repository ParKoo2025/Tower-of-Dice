using System;
using UnityEngine;

public class Player : MonoBehaviour, ICombatant
{
    [SerializeField] private Animator _playerAnimator;
    
    private PlayerStat _playerStat;
    
    private float _attackCooldownTimer = 0.0f;
    private bool _isAttacking = false;

    private float _pendingDamage = 0.0f;
    private float _pendingAocDamage = 0.0f;
    
    public bool IsDead { get; private set; }
    public bool CanAttack => !_isAttacking && _attackCooldownTimer <= 0f;

    public void Init()
    {
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];
        _isAttacking = false;
        IsDead = false;
    }

    public bool TryStartAttack()
    {
        if (!CanAttack) return false;

        _isAttacking = true;
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];

        _pendingDamage = _playerStat.TotalStat[EStatType.AttackDamage];
        _pendingAocDamage = _playerStat.TotalStat[EStatType.AocDamage];
        
        _playerAnimator.Play("ATTACK");

        return true;
    }

    public void OnAttackHit()
    {
        CombatManager.Instance.ProcessPlayerAttack(_pendingDamage, _pendingAocDamage);
    }

    public void OnAttackComplete()
    {
        _isAttacking = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        _playerStat.TotalStat[EStatType.Health] -= damage;

        if (!_isAttacking)
        {
            _playerAnimator.Play("DAMAGED");
        }
        
        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_playerStat.TotalStat[EStatType.Health]})");

        if (_playerStat.TotalStat[EStatType.Health] <= 0.0f)
        {
            IsDead = true;
            _isAttacking = false;
            _playerAnimator.Play("DEATH");
        }
    }

    private void Awake()
    {
        _playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        if (_attackCooldownTimer > 0.0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }
}
    