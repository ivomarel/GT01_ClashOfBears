using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintInput : LintBehaviour
{
    public LayerMask hitMask;
    private Camera mainCam;

    private InputData inputData = new InputData();

    // Update is called once per frame
    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, hitMask))
            {
                //We use CreateAction because we don't want to directly execute action form the Update function (everything should be handled in STEP)
                CreateAction action = new CreateAction();
                action.unitName = "Archer";
                action.position = (LintVector3)hitInfo.point;
                inputData.actions.Enqueue(action);
            }
        }
    }

    public override void Step()
    {
        base.Step();
        while (inputData.actions.Count > 0)
        {
            CreateAction action = inputData.actions.Dequeue();
            CreateUnit(action.unitName, action.position);
        }
    }

    private void CreateUnit(string unitName, LintVector3 pos)
    {
        //Placeholder until we can put this inside out step system
        Unit unitPrefab = Resources.Load<Unit>(unitName);
        Unit unitClone = Instantiate(unitPrefab);
        unitClone.lintTransform.position = pos;
    }
}

public class InputData
{
    public Queue<CreateAction> actions = new Queue<CreateAction>();
}

public class CreateAction
{
    public string unitName;
    public LintVector3 position;
}
