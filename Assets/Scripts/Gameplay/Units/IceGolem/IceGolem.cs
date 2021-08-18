using UnityEngine;

public class IceGolem : Unit
{
    [SerializeField] private uint delayToUndoFreeze = 200;

    [SerializeField] private uint delayToDeath = 200;

    [SerializeField] private GameObject _iceFXPrefab;

    [SerializeField] private GameObject _deathColliderTriggerPrefab;

    [SerializeField] private LintVector3 _spawnOffset;

    private Unit _currentTarget;
    private Animator _currentTargetAnimator;
    private int _currentTargetAttackPower;
    private bool _canFreeze;
    private bool _isDead;
    private GameObject _currentIceFx;

    protected override void Start()
    {
        base.Start();
        _canFreeze = true;
        _isDead = false;
    }

    public override void Step()
    {
        base.Step();

        var newTarget = GetClosestTarget();

        if (_currentTarget == null)
        {
            if (newTarget != null)
                SetCurrentTarget(newTarget);
        }

        if (_currentTarget != null)
        {
            if (InAttackRange())
            {
                OnAttacking();
            }
            else
            {
                OnMovingToTarget();
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    protected override void DoDamage()
    {
        if (_currentTarget != null)
        {
            _currentTarget.OnHit(attackPower);

            if (_currentTarget.health > 0)
            {
                if (_canFreeze)
                    Freeze();
            }
        }
    }

    protected override void Attack()
    {
        if (_isDead) return;
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Linvoke(DoDamage, delayToDoDamageOnAttack);
    }

    public override void OnHit(int amount)
    {
        health -= amount;
        if (health <= 0 && _isDead == false)
        {
            Die();
        }
    }

    protected override void Die()
    {
        _isDead = true;
        anim.SetTrigger("Death");
        GetComponent<LintCollider>().enabled = false;
        var deathColliderTrigger = Instantiate(_deathColliderTriggerPrefab);
        deathColliderTrigger.GetComponent<LintTransform>().position = lintTransform.position;
        deathColliderTrigger.GetComponent<IceGolemDeathTrigger>().TeamNumber = team;
        Linvoke(Death, delayToDeath);
    }

    private void SetCurrentTarget(Unit newTarget)
    {
        _currentTarget = newTarget;
        _currentTargetAttackPower = newTarget.attackPower;
        _currentTargetAnimator = _currentTarget.GetComponentInChildren<Animator>();

        FaceTarget();
    }

    private void FaceTarget()
    {
        LintVector3 dirToTarget = _currentTarget.lintTransform.position - lintTransform.position;
        Lint angle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
        lintTransform.radians.y = angle;
    }

    private void Freeze()
    {
        _canFreeze = false;
        _currentTargetAnimator.speed = 0;
        _currentTarget.attackPower = 0;
        _currentIceFx = Instantiate(_iceFXPrefab);
        _currentIceFx.GetComponent<LintTransform>().position = lintTransform.position + _spawnOffset;
        _currentIceFx.GetComponent<LintTransform>().radians = lintTransform.radians;
        Linvoke(UndoFreeze, delayToUndoFreeze);
    }

    private void UndoFreeze()
    {
        if (_currentTarget != null)
        {
            _currentTarget.attackPower = _currentTargetAttackPower;
            _canFreeze = true;
            _currentTargetAnimator.speed = 1;
        }

        Destroy(_currentIceFx);
    }

    private void Death()
    {
        UndoFreeze();
        Destroy(gameObject);
    }
}