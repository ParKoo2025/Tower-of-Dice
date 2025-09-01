using UnityEngine;

public class Monster : MonoBehaviour, ICombatant
{
    [SerializeField] private Animator _monsterAnimator;
    [SerializeField] private HPController _hpController;
    private MonsterStat _monsterStat;

    private bool _isAttacking = false;
    
    public bool IsDead { get; private set; }

    public void SetStat(StatScriptable stat)
    {
        _monsterStat.SetStat(stat);
    }
    
    public void StartAttack()
    {
        IsDead = false;
        
        _hpController.SetHealth(_monsterStat.CurrentHealth, _monsterStat.TotalStat[EStatType.Health]);
        _hpController.SetAttackSpeed(0f, 1f);
        _isAttacking = true;

        _monsterAnimator.speed = 1f / _monsterStat.TotalStat[EStatType.AttackSpeed];
        
        _monsterAnimator.Play("ATTACK");
    }

    public void OnAttackHit()
    {
        if (IsDead) return;
        
        float damage = _monsterStat.TotalStat[EStatType.AttackDamage];
        CombatManager.Instance.ProcessMonsterAttack(this, damage);
    }
    
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        _monsterStat.CurrentHealth -= damage;
        _hpController.SetHealth(_monsterStat.CurrentHealth, _monsterStat.TotalStat[EStatType.Health]);

        print($"{name}이 {damage}만큼 피해를 입었습니다. (남은 체력 : {_monsterStat.CurrentHealth})");

        if (_monsterStat.CurrentHealth <= 0.0f)
        {
            IsDead = true;
            _monsterAnimator.Play("DEATH");
            _monsterAnimator.ResetTrigger("2_Attack");
        }
    }
    
    public void StopAttack()
    {
        if (IsDead) return;

        _monsterAnimator.Play("IDLE");
    }

    private void Awake()
    {
        _monsterStat = GetComponent<MonsterStat>();
    }

    private void Update()
    {
        if (IsDead) return;
        
        var stateInfo = _monsterAnimator.GetCurrentAnimatorStateInfo(0);
        _hpController.SetAttackSpeed(stateInfo.normalizedTime % 1.0f, 1f);
        
    }
}
