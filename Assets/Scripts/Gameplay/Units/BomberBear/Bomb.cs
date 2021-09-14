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

    private bool _doOnce = false;
    public override void Step()
    {
        base.Step();
        FallDown();
        if (lintTransform.position.y <= 0f && !_doOnce)
        {
            _doOnce = true;
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
        Linvoke(DestroyExplosion, _lifespan);

    }

    private void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }


}
