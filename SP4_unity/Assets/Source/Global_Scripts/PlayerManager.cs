using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager playerManager;

	// private variables
	private enum GameState
	{
		GAMESTATE_LOBBY,
		GAMESTATE_INGAME
	}
	GameState m_gameState;

	struct Player
	{
		public bool player_active;
		public uint player_ID;
		public string player_name;
	}
	private Player[] m_players = new Player[4];


	void Awake()
	{
		if (playerManager == null) {

			DontDestroyOnLoad (gameObject);
			playerManager = this;
		} 
		else if (playerManager != this) 
		{
			Destroy (gameObject);
		}
	}

	private void PlayerAccepted(NetworkingPlayer player, NetWorker sender)
	{
		// Assign this player to a slot and 
		// Send the list of players to this player
		for (int i = 0; i < 4; ++i) 
		{
			if (m_players [i].player_ID != -1)
				continue;
			m_players [i].player_ID = player.NetworkId;
			m_players [i].player_active = true;
			// Send the assigned slot rpc to this player
		}
	}

	public void SendPlayerList(RpcArgs args)
	{
		for (int i = 0; i < 4; ++i) 
		{
			Player curr = args.GetNext<Player> ();
			m_players [i] = curr;
		}
	}

	public void AddPlayer(RpcArgs args)
	{
	}

	public void RemovePlayer(RpcArgs args)
	{
	}
}
