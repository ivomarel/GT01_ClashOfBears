using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintTime : MonoBehaviour
{
    public static uint time;

    private void FixedUpdate()
    {
        time++;
        //TODO Optimize
        LintBehaviour[] lintBehaviours = FindObjectsOfType<LintBehaviour>();
        foreach (LintBehaviour behaviour in lintBehaviours)
        {
            behaviour.Step();
        }
    }
}
