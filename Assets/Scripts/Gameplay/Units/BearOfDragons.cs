using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearOfDragons : Unit
{
    [Header("Tweakables")] public uint fireInterval = 300;
    public uint fireTime = 200;
    
    public uint delayBeforeActualDamaga = 150;
    public LintVector3 fireOffset;
    public Lint fireSpeed;

    [Header("References")] 
    public LintSphereCollider fireSphere;

    public ParticleSystem fireParticles;
    
    
    private uint lastFireTime;
    private bool isSpittingFire;
    private Lint fireLerpValue;
    private int fireDirection = 1;

    protected override void Awake()
    {
        base.Awake();


        //The firesphere should not be parented since parenting does not exist in our system
        fireSphere.transform.SetParent(null);
        fireSphere.gameObject.SetActive(false);
    }

    public override void Step()
    {
        base.Step();
    }

    protected override void OnAttacking()
    {
        if (isSpittingFire)
        {
            //ping pong between these values
            fireLerpValue.value += ((int)fireSpeed * fireDirection);
            if (LintMath.Abs(fireLerpValue) >= LintMath.Float2Lint)
            {
                fireDirection = -fireDirection;
            }

            //TODO instead of pingponging a straight line this should be a curve
            LintVector3 pingpongOffset = fireOffset;
            pingpongOffset.x = fireLerpValue * fireOffset.x;

            fireSphere.lintTransform.position = lintTransform.position + lintTransform.rotationMatrix * pingpongOffset;

            return;
        }

        if (LintTime.time > lastFireTime + fireInterval)
        {
            SpitFire();
        }
        else
        {
            base.OnAttacking();
        }
    }

    private void SpitFire()
    {
        isSpittingFire = true;
        anim.SetTrigger("FireBreath");
        Linvoke(StartFireDamage, delayBeforeActualDamaga);
        Linvoke(StopSpitFire, fireTime);
    }
    private void StartFireDamage()
    {
        fireParticles.Play();
        fireSphere.gameObject.SetActive(true);
    }

    private void StopSpitFire()
    {
        fireSphere.gameObject.SetActive(false);
        fireParticles.Stop();
        isSpittingFire = false;
    }
}