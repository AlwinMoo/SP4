using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class GameLogic : GameLogicBehavior
{
    public static Transform serverTransform;

    //List<GameObject> PlayerCarList;

    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        //PlayerCarList = new List<GameObject>();

        if (PlayerManager.playerManager.networkObject.IsServer)
        {
            //for (int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
            {
                Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                 var newCar = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_car, randpos, transform.rotation, true);

                string playerID = (PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID + 1).ToString();
                 newCar.gameObject.tag = "Player" + playerID;

                networkObject.SendRpc(RPC_SEND_PLAYER_TAG, Receivers.Others, "Player" + playerID);
                 TextDisplay.CarBase = newCar.gameObject;

                newCar.GetComponent<FogOfWarPlayer>().Number = PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID + 1;

            }
        }
        else
        {
            //for (int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
            {
                Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                var newCar = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_car, randpos, transform.rotation, true);

                string playerID = (PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID + 1).ToString();
                newCar.gameObject.tag = "Player" + playerID;

                //itself so no need send message????
                //networkObject.SendRpc(RPC_SEND_PLAYER_TAG, Receivers.Others, "Player" + playerID);
                TextDisplay.CarBase = newCar.gameObject;

                newCar.GetComponent<FogOfWarPlayer>().Number = PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID + 1;

            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
	}

    public override void SendPlayerTag(RpcArgs args)
    {
        if(GameObject.FindGameObjectWithTag("Player").gameObject != null)
            GameObject.FindGameObjectWithTag("Player").gameObject.tag = args.GetNext<string>();
    }
}
