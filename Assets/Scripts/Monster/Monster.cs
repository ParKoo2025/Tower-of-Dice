using System;
using UnityEngine;

public class Monster : MonoBehaviour, ICombatant
{
    [SerializeField] private Animator _monsterAnimator;
    private MonsterStat _monsterStat;
    
    private float _attackCooldownTimer = 0.0f;
    private bool _isAttacking = false;

    private float _pendingDamage;

    public bool IsDead { get; private set; }
    public bool CanAttack => !_isAttacking && _attackCooldownTimer <= 0.0f;

    public void SetStat(StatScriptable stat)
    {
        _monsterStat.SetStat(stat);
    }
    
    public void Init()
    {
        _attackCooldownTimer = _monsterStat.TotalStat[EStatType.AttackSpeed];
        _isAttacking = false;
        IsDead = false;    
    }

    public bool TryStartAttack()
    {
        if (!CanAttack) return false;

        _isAttacking = true;
        _attackCooldownTimer = _monsterStat.TotalStat[EStatType.AttackSpeed];

        _pendingDamage = _monsterStat.TotalStat[EStatType.AttackDamage];
        
        _monsterAnimator.Play("ATTACK");

        return true;
    }

    public void OnAttackHit()
    {
        CombatManager.Instance.ProcessMonsterAttack(this, _pendingDamage);
    }

    public void OnAttackComplete()
    {
        _isAttacking = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        _monsterStat.TotalStat[EStatType.Health] -= damage;

        if (!_isAttacking)
        {
            _monsterAnimator.Play("DAMAGED");
        }
        
        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_monsterStat.TotalStat[EStatType.Health]})");

        if (_monsterStat.TotalStat[EStatType.Health] <= 0.0f)
        {
            IsDead = true;
            _isAttacking = false;
            _monsterAnimator.Play("DEATH");
        }
    }

    private void Awake()
    {
        _monsterStat = GetComponent<MonsterStat>();
    }

    private void Update()
    {
        if (_attackCooldownTimer > 0.0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }
}
