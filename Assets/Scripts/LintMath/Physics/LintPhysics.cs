using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintPhysics : LintBehaviour
{
    public static List<LintCollider> colliders;
    
    private Dictionary<long, CollisionPair> triggerMap = new Dictionary<long, CollisionPair>();


    private void Awake()
    {
        colliders = new List<LintCollider>();
    }

    public override void Step()
    {
        base.Step();

        foreach (CollisionPair pair in triggerMap.Values)
        {
            pair.isColliding = false;
        }
        
        for (int i = 0; i < colliders.Count - 1; i++)
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
                LintCollider c1 = colliders[i];
                LintCollider c2 = colliders[j];

                if (Intersect(c1, c2))
                {
                    //Potentially swap c1 & c2 so the order is always the same (so c2,c1 == c1,c2)
                    if (c1.GetInstanceID() > c2.GetInstanceID())
                    {
                        LintCollider temp = c2;
                        c2 = c1;
                        c1 = temp;
                    }
                    
                    //Get a unique ID by using the two integers and storing them in a long
                    //(by bitshifting the first integer 32 spots it will take the first half of the long, and the second integer will take the second half)
                    long id = ((long)c1.GetInstanceID()) << 32 + c2.GetInstanceID();

                    if (triggerMap.ContainsKey(id))
                    {
                        var x = triggerMap[id];
                        x.isColliding = true;
                    } else { 
                        c1.gameObject.SendMessage("OnLintTriggerEnter", c2, SendMessageOptions.DontRequireReceiver);
                        c2.gameObject.SendMessage("OnLintTriggerEnter", c1, SendMessageOptions.DontRequireReceiver);
                        var p = new CollisionPair() {c1 = c1, c2 = c2, isColliding = true};
                        triggerMap.Add(id, p);
                    }                    

                    c1.gameObject.SendMessage("OnLintTriggerStay", c2, SendMessageOptions.DontRequireReceiver);
                    c2.gameObject.SendMessage("OnLintTriggerStay", c1, SendMessageOptions.DontRequireReceiver);
                }
            }
            
            var noLongerColliding = new List<long>();
            foreach (var kv in triggerMap)
            {
                if (!kv.Value.isColliding)
                {
                    if (kv.Value.c1) kv.Value.c1.gameObject.SendMessage("OnLintTriggerExit", kv.Value.c2, SendMessageOptions.DontRequireReceiver);
                    if (kv.Value.c2) kv.Value.c2.gameObject.SendMessage("OnLintTriggerExit", kv.Value.c1, SendMessageOptions.DontRequireReceiver);
                    //We can't directly remove this from the Dictionary since we can't modify a collection while iterating through it.
                    noLongerColliding.Add(kv.Key);
                }
            }
            //Using the keys from before we can now safely remove these from the Dictionary
            foreach(long key in noLongerColliding)
            {
                triggerMap.Remove(key);
            }
        }
    }

    private static bool Intersect(LintCollider c1, LintCollider c2)
    {
        //'is' means we try and cast it to an IntSphereCollider and return true if successfully cast to it
        if (c1 is LintSphereCollider && c2 is LintSphereCollider)
        {
            //TODO We can optimize this by casting only once (rather than casting once using 'is' and another time using 'as')
            return SphereToSphere(c1 as LintSphereCollider, c2 as LintSphereCollider);
        }
        else if (c1 is LintBoxCollider && c2 is LintBoxCollider)
        {
            return OBBtoOBB(c1 as LintBoxCollider, c2 as LintBoxCollider);
        }
        else if (c1 is LintSphereCollider && c2 is LintBoxCollider)
        {
            return SphereToOBB(c1 as LintSphereCollider, c2 as LintBoxCollider);
        }
        else if (c1 is LintBoxCollider && c2 is LintSphereCollider)
        {
            return SphereToOBB(c2 as LintSphereCollider, c1 as LintBoxCollider);
        }

        return false;
    }

    private static bool SphereToSphere(LintSphereCollider c1, LintSphereCollider c2)
    {
        //Get the offset between the 2 objects
        LintVector3 offset = c1.lintTransform.position - c2.lintTransform.position;

        //Get the two radius' combined
        Lint radiusCombined = c1.radius + c2.radius;

        //Rather than getting the magnitude (which requires a square root operation), we get the squared magnitude
        //Since we compare it to radiusCombined squared, this will give us the same result
        return offset.sqrMagnitude <= radiusCombined * radiusCombined;
    }

    private static bool AABBtoAABB(LintBoxCollider c1, LintBoxCollider c2)
    {
        return (c1.min.x <= c2.max.x && c1.max.x >= c2.min.x) &&
               (c1.min.y <= c2.max.y && c1.max.y >= c2.min.y) &&
               (c1.min.z <= c2.max.z && c1.max.z >= c2.min.z);
    }

    private static bool OBBtoOBB(LintBoxCollider c1, LintBoxCollider c2)
    {
        //This will contain every side of the box
        List<LintVector3> sides = new List<LintVector3>();

        LintVector3[] c1Sides = c1.GetSides();
        LintVector3[] c2Sides = c2.GetSides();

        sides.AddRange(c1Sides);
        sides.AddRange(c2Sides);
        
        //For 3D Collision, we also need to check the CROSS PRODUCT
        sides.Add(LintVector3.Cross(c1Sides[0], c2Sides[0]));
        sides.Add(LintVector3.Cross(c1Sides[0], c2Sides[1]));
        sides.Add(LintVector3.Cross(c1Sides[0], c2Sides[2]));
        sides.Add(LintVector3.Cross(c1Sides[1], c2Sides[0]));
        sides.Add(LintVector3.Cross(c1Sides[1], c2Sides[1]));
        sides.Add(LintVector3.Cross(c1Sides[1], c2Sides[2]));
        sides.Add(LintVector3.Cross(c1Sides[2], c2Sides[0]));
        sides.Add(LintVector3.Cross(c1Sides[2], c2Sides[1]));
        sides.Add(LintVector3.Cross(c1Sides[2], c2Sides[2]));

        LintVector3[] c1Corners = c1.GetCorners();
        LintVector3[] c2Corners = c2.GetCorners();

        for (int i = 0; i < sides.Count; i++)
        {
            LintVector3 side = sides[i];

            MinMax c1MinMax = GetProjectedMinMax(side, c1Corners);
            MinMax c2MinMax = GetProjectedMinMax(side, c2Corners);

            //When one of the minmaxes does NOT overlap, there is no collision
            //No overlap = we can put an 'axis' between them (Separating Axis Theorem)
            if (!c1MinMax.Overlaps(c2MinMax))
            {
                return false;
            }
        }

        return true;
    }

    private static bool SphereToOBB(LintSphereCollider sphere, LintBoxCollider box)
    {
        //we compare it to radius squared
        return (GetDistanceFromOBBToSphere(sphere, box) <= sphere.radius * sphere.radius);
    }
    
    // Returns a Lint which is the distance between the closest point on OBB box from the sphere center
    private static Lint GetDistanceFromOBBToSphere(LintSphereCollider sphere, LintBoxCollider box)
    {
        // A distance vector from the center of the sphere to the OBB box center
        LintVector3 distanceVector = sphere.lintTransform.position - box.lintTransform.position;
        Lint distance = 0;

        // To store projection length
        Lint projection;
        Lint extra = 0;

        //For X axis, we calculate projection using Dot Product of the distance Vector and Right Vector
        projection = LintVector3.Dot(distanceVector, box.GetSides()[0]);
        extra = 0;

        if (projection < box.min.x)
        {
            extra = projection + box.extents.x;
        }
        else if (projection > box.max.x)
        {
            extra = projection - box.extents.x;
        }
        distance += extra * extra;

        //For Y axis, we calculate projection using Dot Product of the distance Vector and Up Vector
        projection = LintVector3.Dot(distanceVector, box.GetSides()[1]);
        extra = 0;

        if (projection < box.min.y)
        {
            extra = projection + box.extents.y;
        }
        else if (projection > box.max.y)
        {
            extra = projection - box.extents.y;
        }
        distance += extra * extra;

        //For Z axis, we calculate projection using Dot Product of the distance Vector and Forward Vector
        projection = LintVector3.Dot(distanceVector, box.GetSides()[2]);
        extra = 0;

        if (projection < box.min.z)
        {
            extra = projection + box.extents.z;
        }
        else if (projection > box.max.z)
        {
            extra = projection - box.extents.z;
        }
        distance += extra * extra;

        return distance;
    }


    private static MinMax GetProjectedMinMax(LintVector3 side, LintVector3[] corners)
    {
        MinMax m = new MinMax();
        m.min = long.MaxValue;
        m.max = long.MinValue;

        foreach (LintVector3 corner in corners)
        {
            Lint positionOnLine = LintVector3.Dot(corner, side);
            if (positionOnLine > m.max)
            {
                m.max = positionOnLine;
            }

            if (positionOnLine < m.min)
            {
                m.min = positionOnLine;
            }
        }

        return m;
    }
}

public struct MinMax
{
    public Lint min;
    public Lint max;

    public bool Overlaps(MinMax other)
    {
        return !(this.min > other.max || this.max < other.min);
    }
}

//Not a struct because we need to modify member values in the Dictionary
public class CollisionPair
{
    public LintCollider c1;
    public LintCollider c2;
    public bool isColliding;

}