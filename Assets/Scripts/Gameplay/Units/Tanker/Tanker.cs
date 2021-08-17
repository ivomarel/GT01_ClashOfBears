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
        if (_wasDead)
        {
            ApplyDie();
        }
        else
        {
            base.Step();
            if (target != null)
            {
                if (InAttackRange())
                {
                    OnKnockBack();
                }
            }
        }
    }

    private void OnKnockBack()
    {
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

        anim.SetTrigger("Dead");
        _lastDeadTime = LintTime.time;
        _wasDead = true;
    }


    private void ApplyDie()
    {
        if (_revivalNumOfTimes > 0)
        {
            if (LintTime.time > _lastDeadTime + _revivalTimers)
            {
                Revival();
                _revivalNumOfTimes--;
                _wasDead = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Revival()
    {
        anim.SetTrigger("Revival");
        health = _healthMax;
        Debug.Log(123);
    }

}
