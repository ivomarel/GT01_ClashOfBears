using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBear : Unit{

	[Header("Heal Bear")]
	[SerializeField]
	private int healPower = 25;

	[SerializeField]
	public Lint healRange;

	//Runtime
	Unit targetAlly;

	override
	protected void Awake(){
		base.Awake();
	}

	override
	protected void Attack(){
		anim.SetTrigger("Attack");
		anim.SetFloat("Speed", 0);
		HealUnits();
	}

	override
	public void Step(){
		base.Step();
		targetAlly = GetClosestTarget(true);
		if (targetAlly != null)
        {
            if (InAttackRange())
            {
                HealUnits();
            }
            else
            {
                OnMovingToTarget();
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

	}
	public void HealUnits(){

		// sphereCollider.enabled = true;
		//Used this in case the cached logic isn't ready yet
		Unit[] units = FindObjectsOfType<Unit>();
		foreach (Unit possibleAlly in units)
        {
			if(InRange(possibleAlly.lintTransform, healRange) && possibleAlly.team == team)
			{
				possibleAlly.OnHeal(healPower);
			}
		}

	}

	protected virtual bool InRange(LintTransform target, Lint range)
    {
        LintVector3 dirToTarget = target.position - lintTransform.position;
        return dirToTarget.sqrMagnitude < range * range;
    }

	private void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, healRange * LintMath.Lint2Float);
	}

}
