using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class LobbyScript : LobbyBehavior {
	public short[] players = new short[4];
	// Use this for initialization
	void Start () {
		if (networkObject.IsServer) {
			networkObject.player1 = 1;
			networkObject.player2 = 0;
			networkObject.player3 = 0;
			networkObject.player4 = 0;
			return;
		}

		int playerNumber = (int)PlayerManager.playerManager.GetPlayerIndex ();
        Debug.Log("I just joined the lobby with playernumber " + playerNumber);
		players [playerNumber] = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// Updating according to server
		players[0] = networkObject.player1;
		players[1] = networkObject.player2;
		players[2] = networkObject.player3;
		players[3] = networkObject.player4;


	}

	// Toggles the ready of the current player
	public void ReadyToggle()
	{
        // Set the current player's ready to be toggled

		uint index = PlayerManager.playerManager.GetPlayerIndex ();
        Debug.Log("Current Players Index :" + index);
		if (players [index] == 2)
			players [index] = 1;
		else
			players [index] = 2;
	}

	public override void BeginReadyCD(RpcArgs args)
	{
	}
}
