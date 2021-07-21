using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDebug : LintBehaviour
{
    public LintVector3 v1;
    public LintVector3 v2;
    //public Lint myLint;
   // public int power;

    private void OnGUI()
    {
        //Draw(LintVector3.Dot(v1, v2));
        //Draw(Vector3.Dot(v1, v2));
        //Attempted same result (as a Lint)
        // GUILayout.Label(LintMath.Asin(myLint).ToString());

        // GUILayout.Label(LintMath.Pow(myLint, power).ToString());

        //Desired result (as a float)
        // GUILayout.Label((Mathf.Asin(myLint * LintMath.Lint2Float) * LintMath.Float2Lint).ToString());

       // LintMatrix coolMatrix = LintMatrix.CreateFromEuler(lintTransform.radians);        

       // Draw(transform.localToWorldMatrix);

      //  Draw(coolMatrix);
    }

    private void Draw(object o)
    {
        GUILayout.Label(o.ToString());
    }

    
}
