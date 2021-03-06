using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LintTime : MonoBehaviour
{
    public static uint time;

    public bool autoRun;

    public static void Step()
    {
        time++;
        //TODO Optimize
        LintBehaviour[] lintBehaviours = FindObjectsOfType<LintBehaviour>();
        foreach (LintBehaviour behaviour in lintBehaviours)
        {
            behaviour.Step();
        }
    }

    private void FixedUpdate()
    {
        if (autoRun)
        {
            Step();
        }
    }

}
