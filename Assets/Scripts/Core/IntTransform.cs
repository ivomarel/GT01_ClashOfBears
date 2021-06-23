using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IntTransform : MonoBehaviour
{
    public IntVector3 position;

    private void Update()
    {
        if (Application.isPlaying)
        {
            //If the application is playing, I can update the Unity position (=visual only) based on the Simulation position, but NEVER the other way around
            transform.position = position;
        }
        else
        {
            //This is ONLY allowed while editing (since otherwise it could break our deterministic simulation)
            position = (IntVector3)transform.position;
        }
    }
}
