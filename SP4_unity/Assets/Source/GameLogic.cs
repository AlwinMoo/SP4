using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class GameLogic : GameLogicBehavior
{
    public static Transform serverTransform;

    GameObject thePlayerInfo;

    //List<GameObject> PlayerCarList;

    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
         var newCar = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_car, randpos, transform.rotation, true);

        //string playerID = (PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID).ToString();
        //newCar.gameObject.tag = "Player" + playerID;

        TextDisplay.CarBase = newCar.gameObject;

        //newCar.GetComponent<FogOfWarPlayer>().Number = PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID;

        var plane = NetworkManager.Instance.InstantiateNetworkMapGeneration(0);
        thePlayerInfo = newCar.gameObject;
    }

	// Update is called once per frame
	void Update ()
    {
        //networkObject.SendRpcUnreliable(RPC_UPDATE_PLAYER_HEALTH, Receivers.All, thePlayerInfo.gameObject.GetComponent<VehicleBase>().health, thePlayerInfo.gameObject.tag);
	}

    public override void UpdatePlayerHealth(RpcArgs args)
    {
        float newHP = args.GetNext<float>();
        string playerTag = args.GetNext<string>();

        GameObject.FindGameObjectWithTag(playerTag).GetComponent<VehicleBase>().health = newHP;
    }
}
