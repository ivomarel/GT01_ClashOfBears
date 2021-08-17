using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : Unit
{
    [Header("Knock Back")]
    [SerializeField]
    private AreaKnockBack _areaKnockBack;
    [SerializeField]
    private uint _knockBackBtwTime = 300;
    [SerializeField]
    private ParticleSystem _knockParticle;

    [Header("Revival")]
    [SerializeField]
    private uint _revivalNumOfTimes = 1;

    [SerializeField]
    private uint _revivalTimers = 3;

    private bool _wasDead;
    private int _healthMax;
    private uint _lastKnockTime;
    private uint _lastDeadTime;

    protected override void Awake()
    {
        base.Awake();
        _wasDead = false;
        _healthMax = health;
    }

    protected override void Start()
    {
        base.Start();

    }

    public override void Step()
    {
        //dead check
        if (_wasDead)
        {
            ApplyDie();
        }
        else
        {
            //if not , do things as normal
            base.Step();
            if (target != null)
            {
                //add a OnKnockBack function as a ability 
                if (InAttackRange())
                {
                    OnKnockBack();
                }
            }
        }
    }

    private void OnKnockBack()
    {
        //do KnockBack every _knockBackBtwTime 
        if (LintTime.time > _lastKnockTime + _knockBackBtwTime)
        {
            _knockParticle.Play();
            var clone = Instantiate(_areaKnockBack);
            clone.team = team;
            clone.lintTransform.position = lintTransform.position;
            clone.lintTransform.radians = lintTransform.radians;
            _lastKnockTime = LintTime.time;
        }
    }

    public override void OnHit(int amount)
    {
        // let unit only call once die function when it was dead
        if (health > 1)
        {
            health -= amount;
        }
        if (health <= 0)
        {
            health = 1;
            Die();
            Debug.Log("die");
        }
    }

    protected override void Die()
    {
        //apply dead animation 
        anim.SetTrigger("Dead");
        //set _lastDeadTime as LintTime.time
        _lastDeadTime = LintTime.time;
        _wasDead = true;
    }


    private void ApplyDie()
    {
        //if have revival times
        if (_revivalNumOfTimes > 0)
        {
            //if over the revival waiting time 
            if (LintTime.time > _lastDeadTime + _revivalTimers)
            {
                //do revival
                Revival();
                //_revivalNumOfTimes -1;
                _revivalNumOfTimes--;
                _wasDead = false;
            }
        }
        //if the unit don't has revival times
        else
        {
            //destroy this 
            Destroy(gameObject);
        }
    }
    private void Revival()
    {
        anim.SetTrigger("Revival");
        //set health as the original health
        health = _healthMax;
    }

}
