using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDebug : MonoBehaviour
{
    public Lint myLint;

    private void OnGUI()
    {
        //Attempted same result (as a Lint)
        GUILayout.Label(LintMath.Sin(myLint).ToString());

        //Desired result (as a float)
        GUILayout.Label(Mathf.Sin(myLint * LintMath.Lint2Float).ToString());
    }
}
