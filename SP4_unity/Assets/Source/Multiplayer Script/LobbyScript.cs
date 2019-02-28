using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class LobbyScript : LobbyBehavior
{
    public static short[] players = new short[3];
    public static bool ready;

    // Use this for initialization
    void Start()
    {
        if (networkObject.IsServer)
        {
            networkObject.player1 = 1;
            networkObject.player2 = 0;
            networkObject.player3 = 0;
            networkObject.player4 = 0;
            return;
        }

        int playerNumber = (int)PlayerManager.playerManager.GetPlayerIndex();
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        networkObject.SendRpc(RPC_TOGGLE_READY, Receivers.Server, (int)PlayerManager.playerManager.GetPlayerIndex());
    }

    // Update is called once per frame
    void Update()
    {
        // Updating according to server
        //Debug.Log(networkObject.player2);
        //Debug.Log("Player 1 " + (networkObject.player1));
        //Debug.Log("Player 2 " + (networkObject.player2));
        //Debug.Log("Player 3 " + (networkObject.player3));
        //Debug.Log("Player 4 " + (networkObject.player4));
        players[0] = networkObject.player2;
        players[1] = networkObject.player3;
        players[2] = networkObject.player4;
        CheckIfAllReady();
    }
    // Toggles the ready of the current player
    public void ReadyToggle()
    {
        Debug.Log("Toggle is being changed");
        // Check if it's the host 
        if (networkObject.IsServer)
        {
            if (networkObject.player1 == 2)
                networkObject.player1 = 1;
            else
                networkObject.player1 = 2;
            return;
        }
        // Set the current player's ready to be toggled through RPC call
        Debug.Log("sending RPC to server");
        networkObject.SendRpc(RPC_TOGGLE_READY, Receivers.Server, (int)PlayerManager.playerManager.GetPlayerIndex());
    }

    public override void BeginReadyCD(RpcArgs args)
    {
    }

    public override void ToggleReady(RpcArgs args)
    {
        // Get arg
        int playerIndex = args.GetNext<int>();
        // Toggle the player based on the index
        Debug.Log("server receiving message");
        Debug.Log(playerIndex);
        switch (playerIndex)
        {
            case 1:
                if (networkObject.player2 == 1)
                    networkObject.player2 = 2;
                else
                {
                    networkObject.player2 = 1;
                    Debug.Log("Player 2 is now joined");
                }
                break;
            case 2:
                if (networkObject.player3 == 1)
                    networkObject.player3 = 2;
                else
                    networkObject.player3 = 1;
                break;
            case 3:
                if (networkObject.player4 == 1)
                    networkObject.player4 = 2;
                else
                    networkObject.player4 = 1;
                break;
        }
    }
    public override void JoinedLobby(RpcArgs args)
    {
        int playerIndex = args.GetNext<int>();
        Debug.Log("Player joined lobby with index " + playerIndex);
        switch (playerIndex)
        {
            case 1:
                networkObject.player2 = 1;
                break;
            case 2:
                networkObject.player3 = 1;
                break;
            case 3:
                networkObject.player4 = 1;
                break;
        }
    }

    public bool CheckIfAllReady()
    {

        if ((networkObject.player2 == 2  || networkObject.player2 == 0) && (networkObject.player3 == 2 || networkObject.player3 == 0) && (networkObject.player4 == 2 || networkObject.player4 == 0))
        {
            ready = true;
        }
        else
        {
            ready = false;
        }
        return ready;
    }

    public override void ChangedCar(RpcArgs args)
    {
        int carID = args.GetNext<int>();
        int playerID = args.GetNext<int>();

        PlayerManager.playerManager.m_players[playerID].player_car = carID;

        if (networkObject.IsServer)
        {
            networkObject.SendRpc(RPC_CHANGED_CAR, Receivers.Others, carID, playerID);
        }
    }

    public void UpdatePlayerCar(int ID)
    {
        networkObject.SendRpc(RPC_CHANGED_CAR, Receivers.All, ID, (int)PlayerManager.playerManager.GetPlayerID((int)PlayerManager.playerManager.GetPlayerIndex()));
    }
}