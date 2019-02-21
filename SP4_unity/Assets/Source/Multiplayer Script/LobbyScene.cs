using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
public class LobbyScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (NetworkManager.Instance.IsServer)
            NetworkManager.Instance.InstantiateLobby();
    }
}