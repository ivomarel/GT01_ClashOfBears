using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        //There should only be 1 RPCSender object in the scene, so ONLY the MasterClient creates one.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("[RPCSender]", Vector3.zero, Quaternion.identity);
        }
    }

    void Update()
    {
        //TEMP for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RPCSender.Instance.GetComponent<PhotonView>().RPC("SendInputData", RpcTarget.All, "Hello gang!");
        }
    }

}
