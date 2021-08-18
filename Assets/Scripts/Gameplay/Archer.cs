using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    public LintVector3 spawnOffset;
    public Arrow arrowPrefab;

    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Linvoke(SpawnArrow, delayToDoDamageOnAttack);
    }

    private void SpawnArrow()
    {
        Arrow arrowClone = Instantiate(arrowPrefab);
        arrowClone.lintTransform.position = lintTransform.position + spawnOffset;
        arrowClone.lintTransform.radians = lintTransform.radians;
    }
}