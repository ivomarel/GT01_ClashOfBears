using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Newtonsoft.Json;
using Photon.Pun.Demo.SlotRacer.Utils;

public class NetworkManager : MonoBehaviour
{
    private uint syncInterval = 10;

    private uint lastSyncTime;

    private RPCSender rpcSender;
    
    // Start is called before the first frame update
    void Start()
    {
        //There should only be 1 RPCSender object in the scene, so ONLY the MasterClient creates one.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("[RPCSender]", Vector3.zero, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (rpcSender == null)
        {
            rpcSender = FindObjectOfType<RPCSender>();
            if (rpcSender == null)
                return;
        }
        
        if (LintTime.time == 0)
        {
            //We send our LocalInput once when LintTime.time = 0.
            SendLocalInput();
        }
        
        if (LintTime.time == lastSyncTime + syncInterval)
        {
            //If we haven't received all the inputs yet - stop the simulation.
            if (!rpcSender.AllInputsReceived(LintTime.time))
            {
                return;
            }
            
            //Process inputs
            List<InputData> inputDatas = rpcSender.GetInputs(LintTime.time);
            foreach (InputData inputData in inputDatas)
            {
                foreach (CreateAction action in inputData.actions)
                {
                    action.Execute();
                }
            }
            

            SendLocalInput();
        }

        LintTime.Step();
    }

    private void SendLocalInput()
    {
        lastSyncTime = LintTime.time;

        //Sending new inputs
        //I.e. when we are syncing in step '10', we send the actions that should be executed in step 20.
        InputManager.Instance.inputData.timeToExecute = LintTime.time + syncInterval;

        //TODO, when serializing, we shouldn't include all get-only properties
        string inputDataJson = JsonConvert.SerializeObject(InputManager.Instance.inputData, Formatting.None,
            new JsonSerializerSettings()
            {
                //This will ignore error messages for our LintVector3.normalized property
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        //RPCSender.Instance.SendInputData(inputDataJson);
        rpcSender.GetComponent<PhotonView>().RPC("SendInputData", RpcTarget.All, inputDataJson);
        
        //Reset the inputData after syncing.
        InputManager.Instance.inputData = new InputData();
    }
}
