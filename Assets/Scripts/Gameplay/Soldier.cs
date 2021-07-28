using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : LintBehaviour
{
    //Tweakables
    public Lint rotateSpeed = 150;
    public Lint moveSpeed = 10;

    public int team = 0;

    //References
    public Renderer[] coloredMeshes;

    //Runtime vars
    private Soldier target;

    private void Start()
    {
        Color teamColor = GameManager.Instance.teamColors[team];

        foreach (Renderer r in coloredMeshes)
        {
            r.material.color = teamColor;
        }
    }

    private void FixedUpdate()
    {
        /*
        lintTransform.radians.y += rotateSpeed;

        LintVector3 direction = new LintVector3();
        direction.z = LintMath.Cos(lintTransform.radians.y);
        direction.x = LintMath.Sin(lintTransform.radians.y);

        lintTransform.position += direction * moveSpeed;
        */
        target = GetClosestTarget();

        if (target != null)
        {
            //dirAtoB = B-A
            LintVector3 dirToTarget = target.lintTransform.position - lintTransform.position;
            Lint angle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
            lintTransform.radians.y = angle;

            lintTransform.position += dirToTarget.normalized * moveSpeed;
        }

    }

    /*
    //VERY WRONG: We don't need to sort the entire array, just get the smallest value!
    private Soldier GetClosestTarget ()
    {
        //Filters units by team. Only check units that are not in the team
        //WRONG: Getting list on runtime every single method call
        //WRONG: FindObjectsOfType every frame is a big nono
        List<Soldier> units = new List<Soldier>(FindObjectsOfType<Soldier>()).FindAll(u => u.team != team);

        if (units.Count == 0)
            return null;
        
        //WRONG: Bubblesort sucks
        //Bubblesort.
        //Big O Notation: O(n^2)
        //n = number of soldiers that are not in my team
        for (int i = 0; i < units.Count - 1; i++)
        {
            //Don't hurt your friends!
            //WRONG: We already checked the team before
            if (units[i].team != team)
            {
                for (int j = 0; j < units.Count - 1; j++)
                {
                    //Calculating distance the 2 units
                    Lint distanceToUnit0 = (units[j].lintTransform.position - this.lintTransform.position).magnitude;
                    Lint distanceToUnit1 = (units[j + 1].lintTransform.position - this.lintTransform.position).magnitude;
                    if (distanceToUnit0 > distanceToUnit1)
                    {
                        //Swap if needed
                        Soldier temp = units[j];
                        units[j] = units[j + 1];
                        units[j + 1] = temp;
                    }
                }
            }
        }
        return units[0];
    }
    */


    //Big O: O(n)
    private Soldier GetClosestTarget ()
    {
        //TODO this should be cached (Soldiers OnEnable should register to GameManager, OnDisable should unregister)
        Soldier[] soldiers = FindObjectsOfType<Soldier>();

        Lint closestDistanceSqrd = long.MaxValue;
        Soldier closestSoldier = null;

        foreach(Soldier soldier in soldiers)
        {
            if (soldier.team != team)
            {
                Lint distanceSqrd = (soldier.lintTransform.position - this.lintTransform.position).sqrMagnitude;
                if (distanceSqrd < closestDistanceSqrd)
                {
                    closestDistanceSqrd = distanceSqrd;
                    closestSoldier = soldier;
                }
            }            
        }

        return closestSoldier;
    }
    
}
