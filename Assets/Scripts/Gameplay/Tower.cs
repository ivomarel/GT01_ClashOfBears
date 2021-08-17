using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    public LintVector3 spawnOffset;
    public CannonBall bombPrefab;

    protected override void Attack()
    {
        Linvoke(Shoot, delayToDoDamageOnAttack);
    }

    private void Shoot()
    {
        if (target != null)
        {
            LintVector3 dirToTarget = target.lintTransform.position - (lintTransform.position + spawnOffset);
            CannonBall bombClone = Instantiate(bombPrefab);
            bombClone.lintTransform.position = lintTransform.position + spawnOffset;
            bombClone.Forward = dirToTarget;
            bombClone.team = team;
        }
    }

    protected override void OnMovingToTarget()
    {
        //Since the tower dont move but is still a unit, I override this function without any logic inside
    }

}
