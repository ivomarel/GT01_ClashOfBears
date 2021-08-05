using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerDebugger : LintBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;

    
    void OnLintTriggerEnter(LintCollider other)
    {
        onTriggerEnter?.Invoke();
    }
    
        
    void OnLintTriggerStay(LintCollider other)
    {
        onTriggerStay?.Invoke();
    }
    
    void OnLintTriggerExit(LintCollider other)
    {
        onTriggerExit?.Invoke();
    }

    
}
