using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Unit
{
    public LintVector3 spawnOffset;
    public Bomb BombPrefab;
    private Unit _currentTarget;

    private bool inCooldown;

    private bool _drop = true;

    [SerializeField]
    private int _dropSpeed = 400;

    [SerializeField]
    private BombExplosion _bombExplosion;

    [SerializeField]
    private GameObject _explosionFX;

    protected override void Start()
    {
        base.Start();
    }

    public override void Step()
    {
        base.Step();

        _currentTarget = GetClosestTarget();

        //  lintTransform.position.y = 50000;

        if (_currentTarget != null && _drop == false)
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

        if (_drop)
        {
            DropDown();
            if (CheckHitGround())
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        var fx = Instantiate(_explosionFX, transform.position, Quaternion.identity);
        fx.GetComponent<LintTransform>().position = lintTransform.position;
        fx.GetComponent<LintTransform>().position.y += spawnOffset.y;
        fx.transform.localScale *= 3;

        var clone = Instantiate(_bombExplosion);
        clone.lintTransform.position = lintTransform.position;
        clone.lintTransform.position.y += spawnOffset.y;

        Destroy(gameObject);
    }

    private bool CheckHitGround()
    {
        if (lintTransform.position.y <= (0 - spawnOffset.y))
        {
            return true;
        }
        return false;
    }

    protected override void Die()
    {
        _drop = true;
    }

    private void DropDown()
    {
        lintTransform.position.y -= _dropSpeed;
    }

    protected override void OnMovingToTarget()
    {
        base.OnMovingToTarget();
        anim.SetFloat("Speed", 0);
    }


    protected override bool InAttackRange()
    {
        if (_currentTarget == null) return false;
        LintVector3 dirToTarget = _currentTarget.lintTransform.position - lintTransform.position;
        dirToTarget.z = 0;
        return dirToTarget.sqrMagnitude <= attackRange * attackRange;
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
        _bomb.lintTransform.position = lintTransform.position + spawnOffset;
        _bomb.lintTransform.radians = lintTransform.radians;
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + ((Vector3)spawnOffset), 0.2f);
    }
}
