using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.SceneManagement;

public class ReadyToggler : MonoBehaviour {

    private GameObject PlayerToggle;
    private Toggle ReadyToggle;

    void Start()
    {
        PlayerToggle = GameObject.FindWithTag("Player1"); // + put the players id in here);
        ReadyToggle = PlayerToggle.GetComponent<Toggle>();
    }

	public void ToggleOnOff()
    {
        if(ReadyToggle.isOn)
        {
            Debug.Log("NotReady");
            ReadyToggle.isOn = false;
        }
        else
        {
            Debug.Log("Ready");
            ReadyToggle.isOn = true;
        }
    }

    public void changeScene()
    {
        if (NetworkManager.Instance.Networker.IsServer)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
