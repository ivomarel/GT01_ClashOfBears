using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LintTransform : MonoBehaviour
{
    public LintVector3 position;
    public LintVector3 radians;

    private void Update()
    {
        if (Application.isPlaying)
        {
            //If the application is playing, I can update the Unity position (=visual only) based on the Simulation position, but NEVER the other way around
            transform.position = position;
            Vector3 r = this.radians;
            transform.eulerAngles = r * Mathf.Rad2Deg;// (2* Mathf.PI) * 360
        }
        else
        {
            //This is ONLY allowed while editing (since otherwise it could break our deterministic simulation)
            position = (LintVector3)transform.position;
            radians = (LintVector3)transform.eulerAngles * Mathf.Deg2Rad;
        }
    }
}
