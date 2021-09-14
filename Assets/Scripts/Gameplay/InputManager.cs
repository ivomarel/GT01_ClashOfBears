using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public LayerMask hitMask;

    public Unit currentUnitPrefab;

    private Camera mainCam;

    public InputData inputData = new InputData();


    public int currentLocalTeam = 0;
    

    // Update is called once per frame
    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentLocalTeam--;
                if (currentLocalTeam < 0) currentLocalTeam = GameManager.Instance.teamColors.Length - 1;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentLocalTeam++;
                if (currentLocalTeam > GameManager.Instance.teamColors.Length - 1) currentLocalTeam = 0;
            }
        }
        
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, hitMask))
            {
                //We use CreateAction because we don't want to directly execute action form the Update function (everything should be handled in STEP)
                CreateAction action = new CreateAction();
                if (currentUnitPrefab.cost <= GameManager.Instance.money)
                {
                    GameManager.Instance.money -= currentUnitPrefab.cost;
                    action.unitName = currentUnitPrefab.name;
                    action.position = (LintVector3)hitInfo.point;
                    inputData.actions.Add(action);
                }
                
            }
        }
    }
    
    void FixedUpdate ()
    {
        if (!PhotonNetwork.IsConnected)
        {
            foreach (CreateAction action in inputData.actions)
            {
                inputData.team = currentLocalTeam;
                action.Execute(inputData.team);
            }
            inputData.actions.Clear();
        }
        
    }
   
    
}

public class InputData
{
    public int team;
    public uint timeToExecute;
    public List<CreateAction> actions = new List<CreateAction>();
}

public class CreateAction
{
    public string unitName;
    public LintVector3 position;
    
    public void Execute(int team)
    {
        Unit unitPrefab = Resources.Load<Unit>($"Units/{unitName}");
        Unit unitClone = GameObject.Instantiate(unitPrefab);
        unitClone.team = team;
        unitClone.lintTransform.position = position;
    }
}
