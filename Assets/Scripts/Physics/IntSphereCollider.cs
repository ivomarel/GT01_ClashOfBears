using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntSphereCollider : IntCollider
{
    public uint radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius * IntMath.Int2Float);
    }
}
