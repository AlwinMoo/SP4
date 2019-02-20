using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.SceneManagement;

public class ReadyToggler : MonoBehaviour {

    public Toggle[] Togglers = new Toggle[4];
    public Toggle ReadyToggle;
    public GameObject LobbysystemPrefab;
    private LobbyScript LobScript;

    void Start()
    {
       
    }

    void Update()
    {
        LobbysystemPrefab = GameObject.FindWithTag("LobbySystem");
        LobScript = LobbysystemPrefab.GetComponent<LobbyScript>();
    }

    public void ToggleOnOff()
    {
        uint index = PlayerManager.playerManager.GetPlayerIndex();
        ReadyToggle = Togglers[index];
        Debug.Log("Toggler" + index);
        if (ReadyToggle.isOn)
        {
            ReadyToggle.isOn = false;
        }
        else
        {
            ReadyToggle.isOn = true;
        }

        LobScript.ReadyToggle();
    }

    public void changeScene()
    {
        if (NetworkManager.Instance.Networker.IsServer)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
