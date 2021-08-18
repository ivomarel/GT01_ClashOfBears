using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffArea))]
public class BuffBear : Unit
{
	[SerializeField] private BuffArea buff;
	
	protected override void DoDamage()
	{
		base.DoDamage();
		buff.SetOwner(this);
		buff.enabled = true;
	}
}