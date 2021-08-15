using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Unit
{
    public LintVector3 spawnOffset;
    public Bomb BombPrefab;
    private Unit _currentTarget;

    [SerializeField]
    private LintVector3 _spawnOffset;

    private bool inCooldown;

    protected override void Start()
    {
        base.Start();
        lintTransform.position.y = 50000;
    }

    public override void Step()
    {
        base.Step();

        if (_currentTarget == null)
        {
            _currentTarget = GetClosestTarget();
        }
        if (_currentTarget != null)
        {
            Debug.Log(_currentTarget.name);
        }
        lintTransform.position.y = 50000;

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
    }


    protected override bool InAttackRange()
    {
        if (_currentTarget == null) return false;
        LintVector3 dirToTarget = _currentTarget.lintTransform.position - lintTransform.position;
        dirToTarget.z = 0;
        return dirToTarget.sqrMagnitude < attackRange * attackRange;
    }

    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Debug.Log("Spawned Bomb");
        Linvoke(SpawnBomb, delayToDoDamageOnAttack);
    }

    private void SpawnBomb()
    {
        Bomb _bomb = Instantiate(BombPrefab);
        _bomb.lintTransform.position = lintTransform.position;
        _bomb.lintTransform.position.y -= 2500;
        _bomb.lintTransform.radians = lintTransform.radians;
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + ((Vector3)_spawnOffset), 0.2f);
    }
}
