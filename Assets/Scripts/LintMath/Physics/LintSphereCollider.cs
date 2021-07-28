using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintSphereCollider : LintCollider
{
    public uint radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius * LintMath.Lint2Float);
    }
}
