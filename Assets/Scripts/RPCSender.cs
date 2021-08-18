using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Photon.Pun;
using UnityEngine;

public class RPCSender : Singleton<RPCSender>
{
    private Dictionary<uint, List<InputData>> timeToInputs = new Dictionary<uint, List<InputData>>();

    [PunRPC]
    public void SendInputData(string inputDataJson)
    {
        InputData inputData = JsonConvert.DeserializeObject<InputData>(inputDataJson);
        
        //If this key doesn't exist, add it (and initialize the InputData list)
        if (!timeToInputs.ContainsKey(inputData.timeToExecute))
        {
            timeToInputs.Add(inputData.timeToExecute, new List<InputData>());
        }
        
        timeToInputs[inputData.timeToExecute].Add(inputData);
    }

    //TODO Sort this list
    public List<InputData> GetInputs(uint time)
    {
        return timeToInputs[time];
    }

    public bool AllInputsReceived(uint time)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return true;
        }

        //Check for > in case someone leaves the room
        //TODO we should check for all player ID's specifically to be safe
        if (timeToInputs[time].Count >= PhotonNetwork.CurrentRoom.Players.Count)
        {
            return true;
        }

        return false;

    }
}
