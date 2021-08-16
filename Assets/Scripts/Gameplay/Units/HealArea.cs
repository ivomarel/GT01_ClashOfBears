using UnityEngine;

[RequireComponent(typeof( LintSphereCollider))]
public class HealArea:LintBehaviour{

	public Lint healthValue;

	LintSphereCollider sphereCollider;

	HealBear myUnit;
	private void Awake()
	{
		myUnit = GetComponentInParent<HealBear>();
	}

	// public void OnLintTriggerEnter(LintCollider other){
	// 	Unit otherUnit = other.GetComponent<Unit>();
	// 	if(otherUnit.team == myUnit.team){
	// 		// otherUnit.Heal(healthValue);
	// 	}
	// }

	public void HealUnits(){

		// sphereCollider.enabled = true;

	}

	 protected virtual bool InAttackRange(LintTransform target)
    {
        LintVector3 dirToTarget = target.position - lintTransform.position;
        return dirToTarget.sqrMagnitude < HealBear.healRange * HealBear.healRange;
    }

}
