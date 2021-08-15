using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : LintBehaviour
{
    public Lint speed = 200;

    public override void Step()
    {
        base.Step();
        FallDown();
    }

    private void FallDown()
    {
        Debug.Log("FALL");
        lintTransform.position.y -= speed;
        Debug.Log(lintTransform.position.y);
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
