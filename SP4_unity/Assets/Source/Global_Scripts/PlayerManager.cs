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
			// Initialize the playerManager
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
			Destroy (gameObject);	// There should only be 1 instance of playerManager
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
			NetworkManager.Instance.Networker.playerDisconnected += PlayerDisconnectedFromServer;
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


	/// <summary>
	/// If a player has been accepted into the server, he will be added
	/// </summary>
	private void PlayerAccepted(NetworkingPlayer player, NetWorker sender)
	{
		MainThreadManager.Run (() => {
			Debug.Log ("this function has been called");
			// Assign this player to a slot and 
			// Send the list of players to this player
			for (int i = 0; i < 4; ++i) {
				if (m_players [i].player_ID == player.NetworkId)
					return;
				if (!m_players [i].player_slot_empty)
					continue;
				++m_playerCount;
				m_players [i].player_ID = player.NetworkId;
				m_players [i].player_name = "Player " + (i + 1);
				m_players [i].player_slot_empty = false;
				m_players [i].player_active = false;

				// Send the assigned slot rpc to all players
					networkObject.SendRpc (RPC_GET_PLAYER_LIST, Receivers.All, 
						Serializer.GetInstance ().Serialize<Player[]> (m_players));
					networkObject.SendRpc (player, RPC_ASSIGN_PLAYER, 
						(uint)i);
				break;
			}
			;
		});
	}
	/// <summary>
	/// Receives the list of players in the session
	/// </summary>
	public override void GetPlayerList(RpcArgs args)
	{
		// Run in the main thread
		MainThreadManager.Run (() => {
			m_players = Serializer.GetInstance ().Deserialize<Player[]> (args.GetNext<Byte[]> ());	// Deserialize the information
			m_playerCount = 0;
			for (int i = 0; i < 4; ++i) {
				if (m_players [i].player_slot_empty)
					continue;
				++m_playerCount;	// Redo the player count
			}
			;
		});
	}
	/// <summary>
	/// RPC to assign player to a slot of the 4 player slots available
	/// </summary>
	public override void AssignPlayer(RpcArgs args)
	{
        // Assigning current player to this player
        MainThreadManager.Run(() => {
            m_playerIndex = args.GetNext<uint>();
            Debug.Log("assigned player Index of " + m_playerIndex);

           // networkObject.SendRpc(LobbyScript.RPC_JOINED_LOBBY, Receivers.Server, (int)m_playerIndex);
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

    public int GetPlayerCar(int index)
    {
        return m_players[index].player_car;
    }

	//TODO: disconnected function
	private void DisconnectedFromServer(NetWorker sender)
	{
        Debug.Log("player disconnected");

        NetworkManager.Instance.Networker.disconnected -= DisconnectedFromServer;

        m_players[m_playerIndex].player_slot_empty = true;
        m_players[m_playerIndex].player_name = "player " + (m_playerIndex + 1);
        m_players[m_playerIndex].player_car = 0;

        MainThreadManager.Run(() =>
        {
            NetworkManager.Instance.Disconnect();
            SceneManager.LoadScene(0);
        });
        // Send the new playerlist to everyone
        networkObject.SendRpc(RPC_GET_PLAYER_LIST, Receivers.All,
            Serializer.GetInstance().Serialize<Player[]>(m_players));
    }

	private void PlayerDisconnectedFromServer(NetworkingPlayer _player, NetWorker _sender)
	{
		Debug.Log ("Player has disconnected");
		NetworkManager.Instance.Networker.playerDisconnected -= PlayerDisconnectedFromServer;
		--m_playerCount;
		for (int i = 0; i < 4; ++i) 
		{
			if (m_players [i].player_ID != _player.NetworkId)
				continue;
			m_players [i].player_name = "empty";
			m_players [i].player_slot_empty = true;
			m_players [i].player_active = false;
			m_players [i].player_car = 0;
			break;
		}
		networkObject.SendRpc(RPC_GET_PLAYER_LIST, Receivers.All,
			Serializer.GetInstance().Serialize<Player[]>(m_players));
		// Delete the car that belongs to the player
		NetworkObject networkObjectToDestroy = null;
		foreach (var no in _sender.NetworkObjectList)
		{
			if (no.Owner == _player)
			{
				//Found him
				networkObjectToDestroy = no;                        
			}
		}
		//Remove the actual network object outside of the foreach loop, as we would modify the collection at runtime elsewise. (could also use a return, too late)
		if (networkObjectToDestroy != null)
		{
			_sender.NetworkObjectList.Remove(networkObjectToDestroy);
			networkObjectToDestroy.Destroy();
		}
	}
}
