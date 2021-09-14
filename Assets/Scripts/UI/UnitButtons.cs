using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButtons : MonoBehaviour
{
    private UnitButton unitButtonOriginal;

    // Start is called before the first frame update
    void Start()
    {
        unitButtonOriginal = GetComponentInChildren<UnitButton>();
        Unit[] unitPrefabs = Resources.LoadAll<Unit>("Units");
        bool firstUnit = true;
        foreach(Unit unitPrefab in unitPrefabs)
        {
            UnitButton unitButtonClone = Instantiate(unitButtonOriginal, unitButtonOriginal.transform.parent);
            unitButtonClone.Init(unitPrefab);
            if (firstUnit)
            {
                unitButtonClone.OnUnitToggle(true);
                firstUnit = false;
            }
        }
        unitButtonOriginal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
