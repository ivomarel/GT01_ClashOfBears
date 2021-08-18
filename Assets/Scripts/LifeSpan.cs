using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : LintBehaviour
{

	[SerializeField]
	private uint _timeToDestroy = 2;

    void Start()
    {
        Linvoke(DestroyMe, _timeToDestroy);
    }

	private void DestroyMe()
	{
		Destroy(gameObject);
	}
}
