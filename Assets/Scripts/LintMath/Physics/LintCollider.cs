using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintCollider : MonoBehaviour
{
    public HashSet<LintCollider> CurrentTriggers;

    public LintVector3 offset;
    
    public LintTransform lintTransform
    {
        get
        {
            //TODO Optimize by caching the reference
            return GetComponent<LintTransform>();
        }
    }


    private void OnEnable()
    {
        LintPhysics.colliders.Add(this);
    }

    private void OnDisable()
    {
        LintPhysics.colliders.Remove(this);
    }
    
    private void Start()
    {
        CurrentTriggers = new HashSet<LintCollider>();
    }

    public void OnIntTriggerStay (LintCollider otherColl)
    {
        Debug.Log($"{this.name} is colliding with {otherColl.name}");
    }
}
