using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntPhysics : MonoBehaviour
{
    public static List<IntCollider> colliders;

    private void Awake()
    {
        colliders = new List<IntCollider>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            for (int j = 0; j < colliders.Count; j++)
            {                
                IntCollider c1 = colliders[i];
                IntCollider c2 = colliders[j];
                //Avoid checking intersection with itself
                if (c1 != c2)
                {
                    if (Intersect(c1, c2))
                    {
                        Debug.Log("Intersection!");
                    }
                }                
            }
        }
    }

    private static bool Intersect (IntCollider c1, IntCollider c2)
    {
        //'is' means we try and cast it to an IntSphereCollider and return true if successfully cast to it
        if (c1 is IntSphereCollider && c2 is IntSphereCollider)
        {
            //TODO We can optimize this by casting only once (rather than casting once using 'is' and another time using 'as')
            return SphereToSphere(c1 as IntSphereCollider, c2 as IntSphereCollider);
        }
        return false;
    }

    private static bool SphereToSphere(IntSphereCollider c1, IntSphereCollider c2)
    {
        //Get the offset between the 2 objects
        IntVector3 offset = c1.intTransform.position - c2.intTransform.position;
        
        //Get the two radius' combined
        uint radiusCombined = c1.radius + c2.radius;

        //Rather than getting the magnitude (which requires a square root operation), we get the squared magnitude
        //Since we compare it to radiusCombined squared, this will give us the same result
        return offset.sqrMagnitude <= radiusCombined * radiusCombined;
    }


}
