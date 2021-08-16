using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchUnit : Unit
{
    [SerializeField] private SkeletonUnit skeleton;
    [SerializeField] private int spawnRadius = 100;
    [SerializeField] private int spellInterval = 100;
    [SerializeField] private uint spawnSkeletonDelay;
    [SerializeField] private LintVector3 projectileSpawnOffset;
    [SerializeField] private FireBallProjectile projectile;
    private uint lastSpellTime = LintTime.time;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Step()
    {
        base.Step();
        // if (LintTime.time > lastSpellTime + spellInterval)
        // {
        //     anim.SetTrigger("Skill");
        //     lastSpellTime = LintTime.time;
        //     Linvoke(SpawnSkeletions, spawnSkeletonDelay);
        // }
    }

    protected void SpawnSkeletions()
    {
        var spawnPosZ = this.lintTransform.position.z + spawnRadius;
        SpawnUnit(skeleton, new LintVector3(this.lintTransform.position.x, 0, spawnPosZ), LintVector3.zero);

        spawnPosZ = this.lintTransform.position.z - spawnRadius;
        SpawnUnit(skeleton, new LintVector3(this.lintTransform.position.x, 0, spawnPosZ), LintVector3.zero);

        var spawnPosX = this.lintTransform.position.x + spawnRadius;
        SpawnUnit(skeleton, new LintVector3(spawnPosX, 0, this.lintTransform.position.z), LintVector3.zero);

        spawnPosX = this.lintTransform.position.x - spawnRadius;
        SpawnUnit(skeleton, new LintVector3(spawnPosX, 0, this.lintTransform.position.z), LintVector3.zero);
    }

    private void SpawnUnit(Unit unit, LintVector3 spawnLoc, LintVector3 spawnRot)
    {
        var go = Instantiate(unit);
        go.lintTransform.position = spawnLoc;
        go.lintTransform.radians = spawnRot;
        go.team = this.team;
    }

    private void SpawnProjectile()
    {
        var go = Instantiate(projectile);
        go.lintTransform.position = this.lintTransform.position;
        go.lintTransform.radians = LintVector3.zero;
        go.lintTransform.radians.y = this.lintTransform.radians.y;
        go.SpawnnerTeam = this.team;
    }

    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Linvoke(SpawnProjectile, delayToDoDamageOnAttack);
    }
}