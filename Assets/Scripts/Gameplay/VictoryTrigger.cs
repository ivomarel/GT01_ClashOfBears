using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : LintBehaviour
{
    public Renderer coloredRenderer;

    public Lint captureRate = 200;

    public static event System.Action<int> onVictory;

    private Lint captureValue;

    private int currentTeam;

    private void OnLintTriggerStay(LintCollider other)
    {
        Unit unit = other.GetComponentInParent<Unit>();
        if (unit)
        {
            if (unit.team == currentTeam)
            {
                captureValue += captureRate;
                if (captureValue == LintMath.Float2Lint)
                {
                    captureValue = LintMath.Float2Lint;
                    onVictory?.Invoke(currentTeam);
                }
            }
            else
            {
                captureValue -= captureRate;
                if (captureValue <= 0)
                {
                    currentTeam = unit.team;
                }
            }
        }
    }

    private void Update()
    {
        coloredRenderer.material.color = Color.Lerp(Color.white, GameManager.Instance.teamColors[currentTeam], captureValue * LintMath.Lint2Float);
    }

}
