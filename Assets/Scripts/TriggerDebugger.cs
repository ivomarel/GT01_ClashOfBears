using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerDebugger : LintBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;

    public bool enableDebug;
    
    void OnLintTriggerEnter(LintCollider other)
    {
        Log("OnLintTriggerEnter", other);
        onTriggerEnter?.Invoke();
    }
    
        
    void OnLintTriggerStay(LintCollider other)
    {
        onTriggerStay?.Invoke();
    }
    
    void OnLintTriggerExit(LintCollider other)
    {
        Log("OnLintTriggerExit", other);

        onTriggerExit?.Invoke();
    }

    private void Log(string type, LintCollider other)
    {
        if (enableDebug)
        {
            Debug.Log($"{this.gameObject.name}.{type}({other.name})");
        }
    }
    
}
