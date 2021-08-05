using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : LintBehaviour
{
    public Lint speed;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
            //Super arrow
            Destroy(unit.gameObject);
            Destroy(this.gameObject);
        }
    }
}
