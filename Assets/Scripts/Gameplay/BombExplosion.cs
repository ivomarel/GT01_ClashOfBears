using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : LintBehaviour
{
    private uint _lifespan = 1;

    private void OnLintTriggerStay(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit)
        {
            Destroy(unit.gameObject);
        }
        Destroy(other);
        Linvoke(DestroyExplosion, _lifespan);
    }

    private void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }
}

