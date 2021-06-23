using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntBoxCollider : IntCollider
{
    public IntVector3 extents = new IntVector3((int)IntMath.Float2Int, (int)IntMath.Float2Int, (int)IntMath.Float2Int);

    public IntVector3 min
    {
        get
        {
            return this.intTransform.position - this.extents;
        }
    }

    public IntVector3 max
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
