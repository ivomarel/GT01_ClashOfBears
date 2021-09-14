using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject victoryPanel;
    public Text moneyText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        VictoryTrigger.onVictory += VictoryTrigger_onVictory;
    }

    private void OnDisable()
    {
        VictoryTrigger.onVictory -= VictoryTrigger_onVictory;
    }

    private void VictoryTrigger_onVictory(int obj)
    {
        victoryPanel.SetActive(true);
        victoryPanel.GetComponentInChildren<Text>().text = $"Team {obj}";
    }

    public void OnBackToLobby ()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            Photon.Pun.PhotonNetwork.LeaveRoom();
        }
        SceneManager.LoadScene("LobbyScene");
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = $"$ {GameManager.Instance.money}";
    }
}
