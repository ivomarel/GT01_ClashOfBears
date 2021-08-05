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
    }
}
