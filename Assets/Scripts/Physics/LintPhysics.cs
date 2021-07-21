using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintPhysics : MonoBehaviour
{
    public static List<LintCollider> colliders;

    private void Awake()
    {
        colliders = new List<LintCollider>();
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
                LintCollider c1 = colliders[i];
                LintCollider c2 = colliders[j];
                
                if (Intersect(c1, c2))
                {
                    c1.OnIntTriggerStay(c2);
                    c2.OnIntTriggerStay(c1);
                }
            }
        }
    }

    private static bool Intersect (LintCollider c1, LintCollider c2)
    {
        //'is' means we try and cast it to an IntSphereCollider and return true if successfully cast to it
        if (c1 is LintSphereCollider && c2 is LintSphereCollider)
        {
            //TODO We can optimize this by casting only once (rather than casting once using 'is' and another time using 'as')
            return SphereToSphere(c1 as LintSphereCollider, c2 as LintSphereCollider);
        } else if (c1 is LintBoxCollider && c2 is LintBoxCollider)
        {
            return OBBtoOBB(c1 as LintBoxCollider, c2 as LintBoxCollider);
        }
        return false;
    }

    private static bool SphereToSphere(LintSphereCollider c1, LintSphereCollider c2)
    {
        //Get the offset between the 2 objects
        LintVector3 offset = c1.lintTransform.position - c2.lintTransform.position;
        
        //Get the two radius' combined
        uint radiusCombined = c1.radius + c2.radius;

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
        //TODO replace List with Array
        List<LintVector3> sides = new List<LintVector3>();

        LintVector3[] c1Sides = c1.GetSides();
        LintVector3[] c2Sides = c2.GetSides();

        sides.AddRange(c1Sides);
        sides.AddRange(c2Sides);

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


    private static MinMax GetProjectedMinMax (LintVector3 side, LintVector3[] corners)
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

    public bool Overlaps (MinMax other)
    {
        return !(this.min > other.max || this.max < other.min);
    }
}
