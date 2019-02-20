using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.SceneManagement;

public class ReadyToggler : MonoBehaviour {

    public Toggle[] Togglers = new Toggle[4];
    private Toggle ReadyToggle;
    public GameObject LobbysystemPrefab;
    private LobbyScript LobScript;

    void Start()
    {
        uint index = PlayerManager.playerManager.GetPlayerIndex();
        Debug.Log("Toggler" + index);
        ReadyToggle = Togglers[index];
    }

    void Update()
    {
        LobbysystemPrefab = GameObject.FindWithTag("LobbySystem");
        LobScript = LobbysystemPrefab.GetComponent<LobbyScript>();
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
