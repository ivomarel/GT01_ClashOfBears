using UnityEngine;

[RequireComponent(typeof(LintSphereCollider))]
public class SpeedArea:LintBehaviour{

	[SerializeField]
	private Lint areaSize;

	HealBear myBear;
	[SerializeField]
	int myTeam;

	private void Awake()
	{
		myBear = GetComponentInParent<HealBear>();
		if(myBear){
			myTeam  = myBear.team;
		}
	}

	public void OnLintTriggerEnter(LintCollider other){
		Debug.Log($"Trigger enter {other.transform.name}", this);
		Unit unit = other.GetComponent<Unit>();
		if(unit.team == myTeam){
			unit.SetMoveSpeed(true);
		}
	}

	public void OnLintTriggerEit(LintCollider other){
		Debug.Log($"Trigger exit {other.transform.name}", this);
		Unit unit = other.GetComponent<Unit>();
		if(unit.team == myTeam){
			unit.SetMoveSpeed(false);
		}
	}

	private void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, areaSize * LintMath.Lint2Float);
	}

}