using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LintTransform : MonoBehaviour
{
    public LintVector3 position;
    public LintVector3 radians;

    private LintVector3 _cachedRadians;
    private LintMatrix _cachedMatrix;
    public LintMatrix rotationMatrix
    {       
        get
        {
            if (radians != _cachedRadians)
            {
                _cachedMatrix = LintMatrix.CreateFromEuler(radians);
                _cachedRadians = radians;
            }
            return _cachedMatrix;
        }
    }


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
            
            //Small tweak to mark this object as dirty properly
#if UNITY_EDITOR
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
#endif
        }
    }
}
