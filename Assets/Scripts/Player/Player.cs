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
    public bool CanAttack => !_isAttacking && _attackCooldownTimer <= 0f && !IsDead;

    public void Init()
    {
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];
        _isAttacking = false;
        IsDead = false;
    }

    public bool TryStartAttack()
    {
        if (!CanAttack || IsDead) return false;

        _isAttacking = true;
        _attackCooldownTimer = _playerStat.TotalStat[EStatType.AttackSpeed];

        _pendingDamage = _playerStat.TotalStat[EStatType.AttackDamage];
        _pendingAocDamage = _playerStat.TotalStat[EStatType.AocDamage];
        
        //_playerAnimator.Play("ATTACK");
        _playerAnimator.SetTrigger("2_Attack");
        return true;
    }

    public void OnAttackHit()
    {
        if (IsDead) return;
        
        print("player OnAttackHit");
        CombatManager.Instance.ProcessPlayerAttack(_pendingDamage, _pendingAocDamage);
    }

    public void OnAttackComplete()
    {
        print("player OnAttackComplete");
        _isAttacking = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        _playerStat.TotalStat[EStatType.Health] -= damage;
        
        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_playerStat.TotalStat[EStatType.Health]})");

        if (_playerStat.TotalStat[EStatType.Health] <= 0.0f)
        {
            IsDead = true;
            _isAttacking = false;
            // 죽는 애니메이션 트리거 설정
            _playerAnimator.Play("DEATH");
            
            // 다른 애니메이션 파라미터들을 리셋하여 죽는 애니메이션이 방해받지 않도록 함
            _playerAnimator.ResetTrigger("2_Attack");

        }
    }

    public void StopAttack()
    {
        if (IsDead) return;

        _isAttacking = false;
        //_playerAnimator.Play("IDLE");
    }

    private void Awake()
    {
        _playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        if (IsDead) return;
        
        if (_attackCooldownTimer > 0.0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }
}
    