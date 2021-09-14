using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalTeamUI : MonoBehaviour
{
    private Text currentTeamText;

    // Start is called before the first frame update
    void Start()
    {
        currentTeamText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTeamText.text = $"Team {InputManager.Instance.currentLocalTeam}";
        currentTeamText.color = GameManager.Instance.teamColors[InputManager.Instance.currentLocalTeam];
    }
}
