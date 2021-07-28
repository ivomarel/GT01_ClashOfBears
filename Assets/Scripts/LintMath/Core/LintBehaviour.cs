using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LintTransform))]
public class LintBehaviour : MonoBehaviour
{
    public LintTransform lintTransform
    {
        get
        {
            if (_lintTransform == null)
            {
                _lintTransform = GetComponent<LintTransform>();
            }
            return _lintTransform;
        }
    }

    private LintTransform _lintTransform;
}
