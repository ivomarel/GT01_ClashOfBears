using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPCSender : Singleton<RPCSender>
{
    [PunRPC]
    public void SendInputData(string inputDataString)
    {
        Debug.Log(inputDataString);
    }
}
