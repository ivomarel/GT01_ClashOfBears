using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaKnockBack : LintBehaviour
{
    public int team;
    [SerializeField]
    private uint _knockTime = 100;
    [SerializeField]
    private uint _range;
    [SerializeField]
    private uint _knockSpeed = 300;
    private Unit _unit;
    private LintVector3 _knockDir;

    public override void Step()
    {
        base.Step();
    }
    private void Start()
    {
        //destry this after _knockTime
        //Destroy(gameObject, _knockTime);
        Linvoke(Destroy, _knockTime);
        //get col of this object
        var col = GetComponent<LintSphereCollider>();
        //set knock range to this col's radius 
        col.radius = _range;
    }

    private void OnLintTriggerStay(LintCollider other)
    {   //check if other has Unit 
        if (other.GetComponent<Unit>())
        {
            //assign it to _unit 
            _unit = other.GetComponent<Unit>();
            //check is it same team with the self unit
            if (other.GetComponent<Unit>().team != team)
            {
                // get the _knockDir
                _knockDir = _unit.lintTransform.position - this.lintTransform.position;
                //add a speed to the _unit
                _unit.lintTransform.position += _knockDir.normalized * _knockSpeed;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range * 0.0001f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
