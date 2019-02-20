using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public struct Player
	{
		public bool player_active;
		public uint player_ID;
		public string player_name;
        public bool player_slot_empty;
        public int player_car;
	}
	public Player[] m_players = new Player[4];
	private uint m_playerIndex;
    public int m_playerCount = 0;
	//private uint m_leader;
	void Awake()
	{
		if (playerManager == null) {

			DontDestroyOnLoad (gameObject);
			playerManager = this;
            for (int i = 0; i < 4; ++i)
            {
                playerManager.m_players[i].player_active = false;
                playerManager.m_players[i].player_slot_empty = true;
				playerManager.m_players[0].player_ID = 0;
                playerManager.m_players[i].player_name = "empty";
                playerManager.m_players[i].player_car = 0;

            }
		} 
		else if (playerManager != this) 
		{
			Destroy (gameObject);
		}
	}

	void Start()
	{
		// If the current networker is the server, then setup the callbacks for when
		// a player connects
		if (NetworkManager.Instance.Networker is IServer)
		{
			// When a player is accepted on the server we need to send them the map
			// information through the rpc attached to this object
			uint id = networkObject.Owner.NetworkId;
			NetworkManager.Instance.Networker.playerAccepted += PlayerAccepted;
			playerManager.m_players[0].player_active = false;
			playerManager.m_players[0].player_slot_empty = false;
			playerManager.m_players[0].player_ID = id;
			playerManager.m_players[0].player_name = "Host";
            m_playerIndex = 0;
            ++m_playerCount;
        }
		else
		{
			NetworkManager.Instance.Networker.disconnected += DisconnectedFromServer;
		}
	}
	private void Update()
	{
		// If the current networker is the server, then setup the callbacks for when
		// a player connects
	}

	private void PlayerAccepted(NetworkingPlayer player, NetWorker sender)
	{
		MainThreadManager.Run (() => {
			Debug.Log ("this function has been called");
			// Assign this player to a slot and 
			// Send the list of players to this player
			for (int i = 0; i < 4; ++i) {
				if (m_players [i].player_ID == player.NetworkId)	// Bad workaround :(
					return;
				if (!m_players [i].player_slot_empty)
					continue;
				++m_playerCount;
				m_players [i].player_ID = player.NetworkId;
				m_players [i].player_name = "Player " + (i + 1);
				m_players [i].player_slot_empty = false;
				m_players [i].player_active = false;

				// Send the assigned slot rpc to this player
					networkObject.SendRpc (RPC_GET_PLAYER_LIST, Receivers.All, 
						Serializer.GetInstance ().Serialize<Player[]> (m_players));
					networkObject.SendRpc (player, RPC_ASSIGN_PLAYER, 
						(uint)i);
				break;
			}
			;
		});
	}

	public override void GetPlayerList(RpcArgs args)
	{
		MainThreadManager.Run (() => {
			m_players = Serializer.GetInstance ().Deserialize<Player[]> (args.GetNext<Byte[]> ());
			m_playerCount = 0;
			for (int i = 0; i < 4; ++i) {
				if (m_players [i].player_slot_empty)
					continue;
				++m_playerCount;
			}
			;
		});
	}

	public override void AssignPlayer(RpcArgs args)
	{
		// Assigning current player to this player
		MainThreadManager.Run (() => {
			m_playerIndex = args.GetNext<uint> ();
		});
	}

    public int GetPlayerCount()
    {
        return m_playerCount;
    }

    public string GetPlayerName(int index)
    {
        return m_players[index].player_name;
    }

    public uint GetPlayerID(int index)
    {
        return m_players[index].player_ID;
    }

    public uint GetPlayerIndex()
    {
        return m_playerIndex;
    }

	//TODO: disconnected function
	private void DisconnectedFromServer(NetWorker sender)
	{
		NetworkManager.Instance.Networker.disconnected -= DisconnectedFromServer;

		MainThreadManager.Run(() =>
			{
				NetworkManager.Instance.Disconnect();
				SceneManager.LoadScene(0);
			});
	}
}
