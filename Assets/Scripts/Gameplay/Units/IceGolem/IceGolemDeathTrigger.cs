using UnityEngine;

public class IceGolemDeathTrigger : LintBehaviour
{
    public int TeamNumber;

    [SerializeField] private int _iceDamage;

    [SerializeField] private uint _lifeSpan;

    [SerializeField] private GameObject _iceGolemDeathFx;

    [SerializeField] private LintVector3 _spawnOffset;

    private GameObject _effect;

    private void Start()
    {
        _effect = Instantiate(_iceGolemDeathFx);
        _effect.GetComponent<LintTransform>().position = lintTransform.position + _spawnOffset;
        Linvoke(RemoveDeathTrigger, _lifeSpan);
    }

    private void OnLintTriggerStay(LintCollider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit != null && unit.team != TeamNumber)
        {
            unit.OnHit(_iceDamage);
        }
    }

    private void RemoveDeathTrigger()
    {
        Destroy(_effect);
        Destroy(gameObject);
    }
}