using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public Image unitImage;
    public Text unitCostText;
    private Unit unitPrefab;

    public void Init(Unit unitPrefab)
    {
        this.unitPrefab = unitPrefab;
        unitImage.sprite = unitPrefab.uiSprite;
        unitCostText.text = unitPrefab.cost.ToString();
    }

    public void OnUnitToggle(bool value)
    {
        if (value)
        {
            InputManager.Instance.currentUnitPrefab = this.unitPrefab;
        }
    }


}
