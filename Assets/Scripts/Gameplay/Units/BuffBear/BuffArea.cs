using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LintTransform), typeof(LintSphereCollider))]
public class BuffArea : MonoBehaviour
{
	[SerializeField] private uint buffFrameDuration = 100; //need to be changed with the steps
	[Range(0, 20)] [SerializeField] private uint attackSpeedIncrease = 5;
	[Range(0, 50)] [SerializeField] private int damageBuff = 15;

	private BuffBear _instigator;
	private static readonly List<Unit> BuffedUnits = new List<Unit>();

	private void TryToBuffUnit(Unit target)
	{
		if (BuffedUnits.Contains(target)) return;

		target.attackPower += damageBuff;
		target.attackInterval -= attackSpeedIncrease;
		BuffedUnits.Add(target);
	}

	public void RemoveBuff(Unit target)
	{
		if (!BuffedUnits.Contains(target)) return;

		target.attackPower -= damageBuff;
		target.attackInterval += attackSpeedIncrease;
		BuffedUnits.Remove(target);
	}

	void OnLintTriggerStay(LintCollider other)
	{
		var unit = other.GetComponent<Unit>();
		if (unit == null && unit.team != _instigator.team) return;

		TryToBuffUnit(unit);
	}

	void OnLintTriggerStop(LintCollider other)
	{
		var unit = other.GetComponent<Unit>();
		if (unit == null && unit.team != _instigator.team) return;

		RemoveBuff(unit);
	}

	public void SetOwner(BuffBear owner)
	{
		_instigator = owner;
	}
}