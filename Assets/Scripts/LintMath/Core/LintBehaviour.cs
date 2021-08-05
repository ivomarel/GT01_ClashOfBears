using System;
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

    private LinkedList<InvokeMethod> invokeMethods = new LinkedList<InvokeMethod>();
    
    public void Linvoke(Action method, uint delay)
    {
        InvokeMethod m = new InvokeMethod() {method = method, timeToRun = LintTime.time + delay};
        
        //Looping through the linked list
        var currentNode = invokeMethods.First;
        while (currentNode != null)
        {
            //Add a node before this
            if (m.timeToRun < currentNode.Value.timeToRun)
            {
                invokeMethods.AddBefore(currentNode, m);
                return;
            }
            currentNode = currentNode.Next;
        }
        //If the timeToRun is greater than all, just add last
        invokeMethods.AddLast(m);
    }

    protected virtual void FixedUpdate()
    {
        //Check greater than in case someone calls Linvoke with 0 delay
        while (invokeMethods.Count > 0 && invokeMethods.First.Value.timeToRun <= LintTime.time)
        {
            invokeMethods.First.Value.method();
            //Remove first element of the list
            invokeMethods.RemoveFirst();
        }
    }

}

public struct InvokeMethod
{
    public Action method;
    public uint timeToRun;
}
