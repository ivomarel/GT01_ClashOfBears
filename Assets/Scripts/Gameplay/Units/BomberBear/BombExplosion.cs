using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : LintBehaviour
{
    public int damage = 50;
    private uint _lifespan = 5;

    private void Start()
    {
        Linvoke(DestroyExplosion, _lifespan);
    }

    private void OnLintTriggerEnter(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit)
        {
            unit.OnHit(damage);
        }
    }

    private void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }
}

