using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToggler : MonoBehaviour
{

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

        for (int i = 0; i < 4; ++i)
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
    }

    public void ToggleOnOff()
    {
        LobScript.ReadyToggle();
    }
}
