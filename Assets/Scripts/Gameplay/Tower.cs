using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    public LintVector3 spawnOffset;
    public CannonBall bombPrefab;
    private Unit _towerTarget;


    protected override void Attack()
    {
        Linvoke(Shoot, delayToDoDamageOnAttack);
    }

    private void Shoot()
    {
        if (target != null)
        {
            LintVector3 dirToTarget = target.lintTransform.position - (lintTransform.position + spawnOffset);
            Lint Yangle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
            Lint Xangle = LintMath.Atan2(dirToTarget.z, dirToTarget.y);
            CannonBall bombClone = Instantiate(bombPrefab);
            bombClone.lintTransform.position = lintTransform.position + spawnOffset;
            bombClone.lintTransform.radians.y = Yangle;
            bombClone.lintTransform.radians.x = -Xangle;
            bombClone.team = team;
        }
    }

    protected override void OnMovingToTarget()
    {

    }

}
