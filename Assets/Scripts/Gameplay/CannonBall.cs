using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : LintBehaviour
{

    //Same basic behaivour as the arrow but implemented for a cannonball that
    //the tower spawns. 
    //To avoid destroying friendly units the team of the tower gets assigned to the cannonball too
    public int team;
    public Lint speed;
    public Lint DamagePower;
    public LintVector3 Forward;
    public override void Step()
    {
        base.Step();
        ShootTarget();
    }

    private void ShootTarget()
    {
        this.lintTransform.position += Forward * speed;
    }

    private void OnLintTriggerEnter(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();
        Tower tower = unit.GetComponent<Tower>();
        if (unit)
        {
            if (unit.team != team)
            {

                if (tower)
                {
                    tower.health -= (int)DamagePower;
                }
                Destroy(unit.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
