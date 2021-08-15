using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : LintBehaviour
{
    //Tweakables
    public EUnitType UnitType;
    public Lint rotateSpeed = 150;
    public Lint moveSpeed = 250;
    public Lint attackRange = 10000;
    public uint attackInterval = 50;
    public uint delayToDoDamageOnAttack = 25;
    public int attackPower = 50;
    public int health = 1000;

    public int team = 0;

    //References
    public Renderer[] coloredMeshes;
    protected Animator anim;

    //Runtime vars
    private Unit target;
    private uint lastAttackTime;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        Color teamColor = GameManager.Instance.teamColors[team];

        foreach (Renderer r in coloredMeshes)
        {
            r.material.color = teamColor;
        }
    }

    protected virtual bool InAttackRange()
    {
        LintVector3 dirToTarget = target.lintTransform.position - lintTransform.position;
        return dirToTarget.sqrMagnitude < attackRange * attackRange;
    }

    public override void Step()
    {
        base.Step();

        /*
            lintTransform.radians.y += rotateSpeed;
    
            LintVector3 direction = new LintVector3();
            direction.z = LintMath.Cos(lintTransform.radians.y);
            direction.x = LintMath.Sin(lintTransform.radians.y);
    
            lintTransform.position += direction * moveSpeed;
            */
        target = GetClosestTarget();

        if (target != null)
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

    protected virtual void OnAttacking()
    {
        if (LintTime.time > lastAttackTime + attackInterval)
        {
            Attack();
            lastAttackTime = LintTime.time;
        }
    }

    protected virtual void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Linvoke(DoDamage, delayToDoDamageOnAttack);
    }

    protected virtual void DoDamage()
    {
        if (target != null)
        {
            target.OnHit(attackPower);
        }
    }

    public virtual void OnHit(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnMovingToTarget()
    {
        //dirAtoB = B-A
        LintVector3 dirToTarget = target.lintTransform.position - lintTransform.position;
        Lint angle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
        lintTransform.radians.y = angle;

        lintTransform.position += dirToTarget.normalized * moveSpeed;
        anim.SetFloat("Speed", 1);
    }

    //Big O: O(n)
    protected virtual Unit GetClosestTarget()
    {
        //TODO this should be cached (Soldiers OnEnable should register to GameManager, OnDisable should unregister)
        Unit[] soldiers = FindObjectsOfType<Unit>();

        Lint closestDistanceSqrd = long.MaxValue;
        Unit closestUnit = null;

        foreach (Unit soldier in soldiers)
        {
            if (soldier.team != team)
            {
                if (GetIsValidTarget(soldier))
                {
                    Lint distanceSqrd = (soldier.lintTransform.position - this.lintTransform.position).sqrMagnitude;
                    if (distanceSqrd < closestDistanceSqrd)
                    {
                        closestDistanceSqrd = distanceSqrd;
                        closestUnit = soldier;
                    }
                }
            }
        }

        return closestUnit;
    }

    protected virtual bool GetIsValidTarget(Unit other)
    {
        switch (UnitType)
        {
            case EUnitType.Melee:
                switch (other.UnitType)
                {
                    case EUnitType.Melee: //MELEE ATTACKING MELEE
                        return true;
                    case EUnitType.Ranged: //MELEE ATTACKING RANGED
                        return true;
                    case EUnitType.Flying: //MELEE ATTACKING FLYING
                        return false;
                }
                break;
            case EUnitType.Ranged:
                switch (other.UnitType)
                {
                    case EUnitType.Melee: //RANGED ATTACKING MELEE
                        return true;
                    case EUnitType.Ranged: //RANGED ATTACKING RANGED
                        return true;
                    case EUnitType.Flying: //RANGED ATTACKING FLYING
                        return true;
                }
                break;
            case EUnitType.Flying:
                switch (other.UnitType)
                {
                    case EUnitType.Melee: //FLYING ATTACKING MELEE
                        return true;
                    case EUnitType.Ranged: //FLYING ATTACKING RANGED
                        return true;
                    case EUnitType.Flying: //FLYING ATTACKING FLYING
                        return false;
                }
                break;
        }
        return false;
    }

}


public class CollisionPair
{
    public bool isColliding;
    public LintCollider c1;
    public LintCollider c2;
}

public enum EUnitType
{
    Melee,
    Ranged,
    Flying
}