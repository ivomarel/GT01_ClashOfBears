using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintBoxCollider : LintCollider
{
    public LintVector3 extents = new LintVector3((int)LintMath.Float2Lint, (int)LintMath.Float2Lint, (int)LintMath.Float2Lint);

    public LintVector3 min
    {
        get
        {
            return this.intTransform.position - this.extents;
        }
    }

    public LintVector3 max
    {
        get
        {
            return this.intTransform.position + this.extents;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, extents*2);

        //Drawing all directions vectors (right, up, forward) on this cube
        foreach(LintVector3 side in GetSides())
        {
            Gizmos.DrawRay(this.transform.position, side);
        }
    }

    public LintVector3[] GetSides ()
    {
        LintMatrix mx = LintMatrix.CreateFromEuler(this.intTransform.radians);

        return new LintVector3[]
        {
            new LintVector3(mx[0,0], mx [1,0], mx[2,0]), //First column = Right Vector
            new LintVector3(mx[0,1], mx [1,1], mx[2,1]), //Second column = Up Vector
            new LintVector3(mx[0,2], mx [1,2], mx[2,2])  //Third column = Forward Vector
        };
    }
}
