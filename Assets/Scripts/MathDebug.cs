using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDebug : MonoBehaviour
{
    //public Lint myLint;
   // public int power;

    private void OnGUI()
    {
        //Attempted same result (as a Lint)
        // GUILayout.Label(LintMath.Asin(myLint).ToString());

        // GUILayout.Label(LintMath.Pow(myLint, power).ToString());

        //Desired result (as a float)
        //  GUILayout.Label((Mathf.Asin(myLint * LintMath.Lint2Float) * LintMath.Float2Lint).ToString());

        LintMatrix coolMatrix = new LintMatrix();
        coolMatrix[2, 2] = 14;

        Draw(transform.localToWorldMatrix);

        Draw(coolMatrix);
    }

    private void Draw(object o)
    {
        GUILayout.Label(o.ToString());
    }

    
}