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
        for (int i = 0; i < colliders.Count-1; i++)
        {
            /* i.e. with 4 colliders
              1 -> 2
              1 -> 3
              1 -> 4
              2 -> 3
              2 -> 4
              3 -> 4
             */
            for (int j = i + 1; j < colliders.Count; j++)
            {                
                IntCollider c1 = colliders[i];
                IntCollider c2 = colliders[j];
                
                if (Intersect(c1, c2))
                {
                    c1.OnIntTriggerStay(c2);
                    c2.OnIntTriggerStay(c1);
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
        } else if (c1 is IntBoxCollider && c2 is IntBoxCollider)
        {
            return AABBtoAABB(c1 as IntBoxCollider, c2 as IntBoxCollider);
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

    private static bool AABBtoAABB(IntBoxCollider c1, IntBoxCollider c2)
    {
        return (c1.min.x <= c2.max.x && c1.max.x >= c2.min.x) &&
                 (c1.min.y <= c2.max.y && c1.max.y >= c2.min.y) &&
                 (c1.min.z <= c2.max.z && c1.max.z >= c2.min.z);
    }


}
