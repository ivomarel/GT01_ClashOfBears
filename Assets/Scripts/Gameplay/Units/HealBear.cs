using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBear : Unit{

	[SerializeField]
	private uint healPower = 25;
	[SerializeField]

	private HealArea area;
	internal static Lint healRange;

	//Runtime
	Unit targetAlly;

	override
	protected void Awake(){
		base.Awake();
		area = GetComponentInChildren<HealArea>();
	}

	override
	protected void Attack(){
		anim.SetTrigger("Attack");
		anim.SetFloat("Speed", 0);
		HealAllies();
	}

	override
	public void Step(){
		base.Step();
		// targetAlly = GetClosestTarget(true);

	}

	private void HealAllies(){

	}


}
