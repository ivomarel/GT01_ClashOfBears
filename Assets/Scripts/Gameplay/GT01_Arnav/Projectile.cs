using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : LintBehaviour
{
    public Lint speed = 10;
    public int Damage = 10;
    public Unit target;
    public uint LifeTime = 300;
    [HideInInspector] public int SpawnnerTeam;
    LintVector3 dir;

    private void Start()
    {
        Linvoke(KillMe, LifeTime);
        LintMatrix mx = this.lintTransform.rotationMatrix;
        dir = new LintVector3(mx[0, 2], mx[1, 2], mx[2, 2]);
    }

    private void KillMe ()
    {
        Destroy(gameObject);
    }

    public override void Step()
    {
        base.Step();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        //Move forward
        if (target != null)
        {
            dir = (target.GetTargetPosition() - this.lintTransform.position).normalized;
            Lint angle = LintMath.Atan2(dir.z, dir.x);
            lintTransform.radians.y = angle;
        }    
        this.lintTransform.position += dir * speed;
    }

    private void OnLintTriggerEnter(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit)
        {
            if (unit.team != SpawnnerTeam)
            {
                //Debug.Log($"unit.name = {other.gameObject.name}");
                Destroy(this.gameObject);
                unit.OnHit(Damage);
            }
        }
    }
}