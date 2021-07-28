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
            return this.lintTransform.position - this.extents;
        }
    }

    public LintVector3 max
    {
        get
        {
            return this.lintTransform.position + this.extents;
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

        foreach (LintVector3 corner in GetCorners())
        {
            Gizmos.DrawSphere(corner, 0.1f);
        }
    }

    public LintVector3[] GetSides ()
    {        
        LintMatrix mx = this.lintTransform.rotationMatrix;

        return new LintVector3[]
        {
            new LintVector3(mx[0,0], mx [1,0], mx[2,0]), //First column = Right Vector
            //new LintVector3(mx[0,1], mx [1,1], mx[2,1]), //Second column = Up Vector
            new LintVector3(mx[0,2], mx [1,2], mx[2,2])  //Third column = Forward Vector
        };
    }

    public LintVector3[] GetCorners ()
    {
        LintMatrix mx = this.lintTransform.rotationMatrix;

        //Multiplying any point in space by the rotation matrix, will give us that point as it were rotated by that matrix
        return new LintVector3[]
        {
            lintTransform.position + mx * new LintVector3(extents.x, extents.y, extents.z),
            lintTransform.position + mx * new LintVector3(extents.x, extents.y, -extents.z),
            lintTransform.position + mx * new LintVector3(-extents.x, extents.y, -extents.z),
            lintTransform.position + mx * new LintVector3(-extents.x, extents.y, extents.z)
        };
    }
}
