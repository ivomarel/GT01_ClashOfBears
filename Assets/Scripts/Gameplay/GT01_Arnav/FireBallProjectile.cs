using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : LintBehaviour
{
    public Lint speed = 10;
    public int Damage = 10;
    [HideInInspector] public int SpawnnerTeam;

    public override void Step()
    {
        base.Step();
        MoveForward();
    }

    private void MoveForward()
    {
        //TODO get forward direction of arrow
        //Move forward
        LintMatrix mx = this.lintTransform.rotationMatrix;
        LintVector3 forward = new LintVector3(mx[0, 2], mx[1, 2], mx[2, 2]);
        this.lintTransform.position += forward * speed;
    }

    private void OnLintTriggerEnter(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit)
        {
            if (unit.team != SpawnnerTeam)
            {
                Debug.Log($"unit.name = {other.gameObject.name}");
                Destroy(this.gameObject);
                unit.OnHit(Damage);
            }
        }
    }
}