using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    public int fireDamagePerFrame = 1;
    
    private BearOfDragons owner;
    
    private void Awake()
    {
        owner = GetComponentInParent<BearOfDragons>();
    }


    void OnLintTriggerStay(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null && unit.team != this.owner.team)
        {
            unit.OnHit(fireDamagePerFrame);
        }
    }
}
