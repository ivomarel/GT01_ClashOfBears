using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaKnockBack : LintBehaviour
{
    public int team;
    [SerializeField]
    private float _knockTime = 3;
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
        Destroy(gameObject, _knockTime);
        var col = GetComponent<LintSphereCollider>();
        col.radius = _range;
        Debug.Log(team);
    }

    private void OnLintTriggerStay(LintCollider other)
    {
        if (other.GetComponent<Unit>())
        {
            _unit = other.GetComponent<Unit>();
            if (other.GetComponent<Unit>().team != team)
            {
                Debug.Log(_unit.name);
                _knockDir = _unit.lintTransform.position - this.lintTransform.position;
                _unit.lintTransform.position += _knockDir * _knockSpeed;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range * 0.0001f);
    }
    // private void KnockBack()
    // {
    //     if (_unit != null && team != _unit.team)
    //     {
    //         Debug.Log(_unit.name);
    //         _knockDir = _unit.lintTransform.position - this.lintTransform.position;
    //         _unit.lintTransform.position += _knockDir * _knockSpeed;
    //     }
    // }

}
