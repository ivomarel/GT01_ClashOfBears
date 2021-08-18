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
    [SerializeField] private ParticleSystem spellParticle;
    private uint lastSpellTime = LintTime.time;
    private Lint ogMoveSpeed;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        ogMoveSpeed = moveSpeed;
        CastSpell();
    }

    public override void Step()
    {
        base.Step();

        if (LintTime.time > lastSpellTime + spellInterval)
        {
            CastSpell();
        }
    }

    /// <summary>
    /// Logic to cast a skill
    /// </summary>
    protected void CastSpell()
    {
        ogMoveSpeed = moveSpeed;
        moveSpeed = 0;
        spellParticle.gameObject.SetActive(true);
        anim.SetTrigger("Skill");


        lastSpellTime = LintTime.time;
        Linvoke(SpawnSkeletions, spawnSkeletonDelay);
    }

    /// <summary>
    /// Spawns skeletions in four direction of the witch
    /// </summary>
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

        moveSpeed = ogMoveSpeed;
    }

    /// <summary>
    /// Instantiates any Unit and sets it rotation and position and sets it team to the spawners team.
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="spawnLoc"></param>
    /// <param name="spawnRot"></param>
    private void SpawnUnit(Unit unit, LintVector3 spawnLoc, LintVector3 spawnRot)
    {
        var go = Instantiate(unit);
        go.lintTransform.position = spawnLoc;
        go.lintTransform.radians = spawnRot;
        go.team = this.team;
    }

    /// <summary>
    /// Instantiates projectile and sets it rotation and position and sets it team to the spawners team.
    /// </summary>
    private void SpawnProjectile()
    {
        var go = Instantiate(projectile);
        go.lintTransform.position = this.lintTransform.position + projectileSpawnOffset;
        go.lintTransform.radians = LintVector3.zero;
        go.lintTransform.radians.y = this.lintTransform.radians.y;
        go.SpawnnerTeam = this.team;
    }

    //Override function from unit to linvoke different function.
    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetFloat("Speed", 0);
        Linvoke(SpawnProjectile, delayToDoDamageOnAttack);
    }
}