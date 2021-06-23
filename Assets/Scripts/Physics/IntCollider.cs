using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCollider : MonoBehaviour
{
    public IntTransform intTransform
    {
        get
        {
            //TODO Optimize by caching the reference
            return GetComponent<IntTransform>();
        }
    }


    private void OnEnable()
    {
        IntPhysics.colliders.Add(this);
    }

    private void OnDisable()
    {
        IntPhysics.colliders.Remove(this);
    }

    public void OnIntTriggerStay (IntCollider otherColl)
    {
        Debug.Log($"{this.name} is colliding with {otherColl.name}");
    }
}
