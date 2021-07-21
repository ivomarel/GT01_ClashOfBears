using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : LintBehaviour
{
    public Lint rotateSpeed = 150;
    public Lint moveSpeed = 10;

    public Soldier target;

    private void FixedUpdate()
    {
        /*
        lintTransform.radians.y += rotateSpeed;

        LintVector3 direction = new LintVector3();
        direction.z = LintMath.Cos(lintTransform.radians.y);
        direction.x = LintMath.Sin(lintTransform.radians.y);

        lintTransform.position += direction * moveSpeed;
        */
        if (target != null)
        {
            //dirAtoB = B-A
            LintVector3 dirToTarget = target.lintTransform.position - lintTransform.position;
            Lint angle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
            lintTransform.radians.y = angle;

            lintTransform.position += dirToTarget * moveSpeed;
        }

    }

    private void OnLintTriggerEnter (LintCollider c)
    {
        Debug.Log(1230);
    }
    
}
