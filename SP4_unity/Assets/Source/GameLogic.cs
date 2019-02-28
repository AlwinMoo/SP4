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
    public static float respawnTimer;

    private float HPUpdateDebounce;
    //List<GameObject> PlayerCarList;

    // Use this for initialization
    void Start()
    {
        HPUpdateDebounce = 0f;
        respawnTimer = 0;
        Random.InitState((int)System.DateTime.Now.Ticks);

        Vector3 randpos = new Vector3(Random.Range(-50, -51), 0, Random.Range(-50, -51));
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
        HPUpdateDebounce += Time.deltaTime;
         
        if((thePlayerInfo.gameObject.GetComponent(typeof(Collider)) as Collider).enabled == false)
        {
            respawnTimer += Time.deltaTime;
            Debug.Log(respawnTimer);
            if (respawnTimer >= 3)
            {
                TextDisplay.IsAlive();
                thePlayerInfo.GetComponent<VehicleBase>().SetComponentActive(true);
                networkObject.SendRpc(RPC_SET_PLAYER_ACTIVE, Receivers.All, thePlayerInfo.tag, true);
                thePlayerInfo.GetComponent<VehicleBase>().health = thePlayerInfo.GetComponent<VehicleBase>().maxHealth;
                networkObject.SendRpcUnreliable(RPC_UPDATE_PLAYER_HEALTH, Receivers.All, thePlayerInfo.gameObject.GetComponent<VehicleBase>().health, thePlayerInfo.gameObject.tag);

                respawnTimer = 0;
            }
        }

        if (HPUpdateDebounce > 2f)
        {
            networkObject.SendRpcUnreliable(RPC_UPDATE_PLAYER_HEALTH, Receivers.All, thePlayerInfo.gameObject.GetComponent<VehicleBase>().health, thePlayerInfo.gameObject.tag);
            HPUpdateDebounce = 0f;
        }
	}

    public override void UpdatePlayerHealth(RpcArgs args)
    {
        float newHP = args.GetNext<float>();
        string playerTag = args.GetNext<string>();

        if (GameObject.FindGameObjectWithTag(playerTag) != null)
            GameObject.FindGameObjectWithTag(playerTag).GetComponent<VehicleBase>().health = newHP;
    }

    public override void SetPlayerActive(RpcArgs args)
    {
        string playerTag = args.GetNext<string>();
        bool activeStatus = args.GetNext<bool>();

        GameObject.FindGameObjectWithTag(playerTag).GetComponent<VehicleBase>().SetComponentActive(true);
    }
}
