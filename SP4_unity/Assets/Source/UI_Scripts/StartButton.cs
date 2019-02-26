using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    public GameObject startButton;
    public GameObject readyButton;

    public Toggle[] Togglers = new Toggle[3];
    public GameObject LobbysystemPrefab;
    private LobbyScript LobScript;


    // Use this for initialization
    void Start () {
		if(PlayerManager.playerManager.networkObject.IsServer)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
        }
	}

    void Update()
    {
        LobbysystemPrefab = GameObject.FindWithTag("LobbySystem");
        LobScript = LobbysystemPrefab.GetComponent<LobbyScript>();


        for (int i = 0; i < 2; ++i)
        {
            if (LobbyScript.players[i] == 2)
            {
                Togglers[i].isOn = true;
            }
            else
            {
                Togglers[i].isOn = false;
            }
        }

        if (LobbyScript.ready)
        {
            startButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            startButton.GetComponent<Button>().interactable = false;
        }   
    }

    public void changeScene()
    {
        if(startButton.GetComponent<Button>().interactable)
        {
            if (PlayerManager.playerManager.networkObject.IsServer)
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}
