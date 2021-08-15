using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : LintBehaviour
{
    public Lint speed = 200;
    private uint _lifespan = 1;

    [SerializeField]
    private BombExplosion _bombExplosion;

    [SerializeField]
    private GameObject _explosionFX;

    public override void Step()
    {
        base.Step();
        FallDown();
        if (lintTransform.position.y <= 0f)
        {
            ExplodeFX();
        }
    }

    private void FallDown()
    {
        lintTransform.position.y -= speed;
    }

    private void ExplodeFX()
    {
        Instantiate(_explosionFX, transform.position, Quaternion.identity);
        var clone = Instantiate(_bombExplosion);
        clone.lintTransform.position = lintTransform.position;
    }

    private void OnLintTriggerEnter(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit)
        {
            ExplodeFX();
            Linvoke(DestroyExplosion, _lifespan);
        }
    }

    private void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }


}
