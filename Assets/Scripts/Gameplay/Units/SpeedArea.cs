using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LintSphereCollider))]
public class SpeedArea:LintBehaviour{

	[SerializeField]
	private Lint areaSize;

	HealBear myBear;
	[SerializeField]
	int myTeam;

	LintSphereCollider sphereCollider;

	List<Unit> buffedUnits = new List<Unit>();


	private void Awake()
	{
		myBear = GetComponentInParent<HealBear>();
		if(myBear){
			myTeam  = myBear.team;
		}
		sphereCollider = GetComponent<LintSphereCollider>();
		if(sphereCollider){
			sphereCollider.radius = areaSize;
		}
	}

	public void OnLintTriggerEnter(LintCollider other){
		// Debug.Log($"Trigger enter {other.transform.name}", this);
		Unit unit = other.GetComponent<Unit>();
		if(unit){
			if(unit.team == myTeam && unit != myBear){
				unit.SetMoveSpeed(true);
				if(!buffedUnits.Contains(unit)){
					buffedUnits.Add(unit);
				}
			}
		}
	}

	public void OnLintTriggerExit(LintCollider other){
		// Debug.Log($"Trigger exit {other.transform.name}", this);
		if(other == null){
			return;
		}

		Unit unit = other.GetComponent<Unit>();
		if(unit && unit.team == myTeam && unit != myBear){
			unit.SetMoveSpeed(false);
			if(buffedUnits.Contains(unit) && unit != null){
				buffedUnits.Remove(unit);
			}
		}
	}

	private void OnDestroy(){
		foreach(Unit unit in buffedUnits){
			//Nullcheck in case it was destroyed
			if(unit){
				unit.SetMoveSpeed(false);
			}
		}
	}

	private void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(lintTransform.position, areaSize * LintMath.Lint2Float);
	}

}
