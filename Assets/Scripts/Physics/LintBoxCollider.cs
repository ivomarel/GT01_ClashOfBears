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
    }
}
