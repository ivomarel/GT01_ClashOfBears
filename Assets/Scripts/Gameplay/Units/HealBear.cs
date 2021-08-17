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
	LintTransform speedArea;

	[SerializeField]
	LintBehaviour _particlePrefab;

	override
	protected void Awake(){
		base.Awake();
		speedArea = GetComponentInChildren<SpeedArea>().lintTransform;
	}

	override
	public void Step(){
		target = GetClosestTarget(true);

		if (target != null)
        {
            if (InAttackRange())
            {
                OnAttacking();
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
		speedArea.position = lintTransform.position;

	}

	// private void OnGUI()
	// {
	// 	GUILayout.Label($"My target: {target.transform.name}");
	// }

	override
	protected void Attack(){
        anim.SetFloat("Speed", 0);
		anim.SetTrigger("Attack");
        // Linvoke(HealUnits, delayToDoDamageOnAttack);
		HealUnits();
	}

	public void HealUnits(){

		// sphereCollider.enabled = true;
		//Used this in case the cached logic isn't ready yet
		Unit[] units = FindObjectsOfType<Unit>();
		// Debug.Log($"Heal, units: {units.Length}", this);
		foreach (Unit possibleAlly in units)
        {
			if(InRange(possibleAlly.lintTransform, healRange) &&  possibleAlly.team == team && possibleAlly != this)
			{
				Debug.Log($"Healing {possibleAlly.transform.name}", possibleAlly.transform);
				possibleAlly.OnHeal(healPower);
				//TODO: replace for RPC proton call
				LintBehaviour lintParticles = Instantiate(_particlePrefab);
				lintParticles.lintTransform.position = possibleAlly.lintTransform.position;
				
			}
		}

	}



	protected virtual bool InRange(LintTransform target, Lint range)
    {
        LintVector3 dirToTarget = target.position - lintTransform.position;

        bool isInRange = dirToTarget.sqrMagnitude < range * range;

		return isInRange;
    }

	private void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(lintTransform.position, healRange * LintMath.Lint2Float);
	}

}
