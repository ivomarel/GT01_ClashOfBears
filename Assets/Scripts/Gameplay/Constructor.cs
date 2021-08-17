using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : Unit
{
    private LintVector3 _towerPosition;
    private Tower _towerToFix;
    public uint delayToBuild = 100;
    public Lint BuildingRange = 100000;
    private MeshCollider _battleField;
    [SerializeField] private Tower _towerPrefab;
    private uint _buildProgressValue;
    private uint _buildTimer;
    private uint _buildTime = 500;
    private int _maxHealth;


    protected override void Start()
    {
        base.Start();
        _maxHealth = health;
        _battleField = FindObjectOfType<MeshCollider>();
        _towerPosition = GetNewTowerPos();
    }

    public override void Step()
    {
        //base.Step();

        //is there any towers around you that need help
        _towerToFix = GetTowerToFix();
        if (_towerToFix != null)
        {
            _towerPosition = _towerToFix.lintTransform.position;
            OnMovingToTarget();

            if (InCloseRange())
            {
                FixTower();
            }

        }
        else if (ItCanBuild(_towerPosition))
        {
            OnMovingToTarget();

            if (InCloseRange())
            {
                BuildTower();
            }

        }
    }
    protected virtual bool InCloseRange()
    {
        LintVector3 dirToTarget = _towerPosition - lintTransform.position;
        return dirToTarget.sqrMagnitude < attackRange * attackRange;
    }

    private bool ItCanBuild(LintVector3 TowerPos)
    {
        Tower[] towers = FindObjectsOfType<Tower>();

        foreach (Tower tower in towers)
        {
            Lint distanceSqrd = (tower.lintTransform.position - TowerPos).sqrMagnitude;
            if (distanceSqrd < BuildingRange)
            {
                _towerPosition = GetNewTowerPos();
                return false;
            }

        }

        return true;
    }

    protected override void OnMovingToTarget()
    {
        //dirAtoB = B-A
        LintVector3 dirToTarget = _towerPosition - lintTransform.position;
        Lint angle = LintMath.Atan2(dirToTarget.z, dirToTarget.x);
        lintTransform.radians.y = angle;

        lintTransform.position += dirToTarget.normalized * moveSpeed;
        anim.SetFloat("Speed", 1);
    }

    private LintVector3 GetNewTowerPos()
    {
        var X = Random.Range(-_battleField.transform.localScale.x, _battleField.transform.localScale.x) * LintMath.Float2Lint;
        var Z = Random.Range(-_battleField.transform.localScale.z, _battleField.transform.localScale.z) * LintMath.Float2Lint;
        LintVector3 pos = new LintVector3((Lint)X, 0, (Lint)Z);

        return pos;
    }
    private Tower GetTowerToFix()
    {
        //TODO this should be cached (Soldiers OnEnable should register to GameManager, OnDisable should unregister)
        Tower[] towers = FindObjectsOfType<Tower>();

        Lint closestDistanceSqrd = long.MaxValue;
        Tower closestTower = null;

        foreach (Tower tower in towers)
        {
            if (tower.team == team)
            {
                Lint distanceSqrd = (tower.lintTransform.position - this.lintTransform.position).sqrMagnitude;
                if (distanceSqrd < closestDistanceSqrd)
                {
                    if (tower.health < _maxHealth)
                    {
                        closestDistanceSqrd = distanceSqrd;
                        closestTower = tower;
                    }
                }
            }
        }

        return closestTower;
    }
    private void BuildTower()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);

        if (LintTime.time > _buildTimer)
        {
            _buildProgressValue++;
            //print(_buildProgressValue);
            if (_buildProgressValue == _buildTime)
            {
                //print(_buildProgressValue);
                SpawnTower();
                _buildProgressValue = 0;
            }
            _buildTimer = LintTime.time;
        }

    }
    private void FixTower()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);

        //var _valueToHeal = _maxHealth - _towerToFix.health / _buildTime - _buildProgressValue;
        if (LintTime.time > _buildTimer)
        {
            _buildProgressValue++;
            //_towerToFix.health += (int)_valueToHeal;

            if (_buildProgressValue == _buildTime)
            {
                _towerToFix.health = _maxHealth;
                _towerToFix = null;
                _buildProgressValue = 0;
            }
            _buildTimer = LintTime.time;
        }
    }
    private void SpawnTower()
    {
        Tower towerClone = Instantiate(_towerPrefab);
        towerClone.lintTransform.position = _towerPosition;
        towerClone.team = team;
        _towerPosition = GetNewTowerPos();
    }

}
