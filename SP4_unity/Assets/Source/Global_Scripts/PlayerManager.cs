using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : PlayerManagerBehavior {
	public static PlayerManager playerManager;

	// private variables
	private enum GameState
	{
		GAMESTATE_LOBBY,
		GAMESTATE_INGAME
	}
	GameState m_gameState;

	[System.Serializable]
	struct Player
	{
		public bool player_active;
		public uint player_ID;
		public string player_name;
	}
	private Player[] m_players = new Player[4];
	private uint m_playerIndex;
	//private uint m_leader;
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
			if (!m_players [i].player_active)
				continue;
			m_players [i].player_ID = player.NetworkId;
			m_players [i].player_name = "Player " + i;
			m_players [i].player_active = true;
			// Send the assigned slot rpc to this player
			networkObject.SendRpc( RPC_GET_PLAYER_LIST, Receivers.All, 
				Serializer.GetInstance().Serialize<Player[]>(m_players));
			networkObject.SendRpc(player, RPC_ASSIGN_PLAYER, 
				(uint)i);
		}
	}

	public override void GetPlayerList(RpcArgs args)
	{
		m_players = Serializer.GetInstance ().Deserialize<Player[]>(args.GetNext<Byte[]> ());
	}

	public override void AssignPlayer(RpcArgs args)
	{
		// Assigning current player to this player
		m_playerIndex = args.GetNext<uint>();
	}

	//TODO: disconnected function
}
