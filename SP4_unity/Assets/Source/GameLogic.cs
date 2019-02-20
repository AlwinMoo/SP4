using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class GameLogic : MonoBehaviour
{
    public static Transform serverTransform;

    //List<GameObject> PlayerCarList;

    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        //PlayerCarList = new List<GameObject>();

        StartCoroutine("SpawnPlayer");
    }

    public IEnumerator SpawnPlayer()
    {
        if (PlayerManager.playerManager.networkObject.IsServer)
        {
            for (int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
            {
                Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                var newCar = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[i].player_car, randpos, transform.rotation, true);

                //string searchTerm = "Player" + (i + 1);
                //for (int j = 0; j < UnityEditorInternal.InternalEditorUtility.tags.Length; ++j)
                //{
                //    if (UnityEditorInternal.InternalEditorUtility.tags[j].Contains(searchTerm))
                //        newCar.tag = UnityEditorInternal.InternalEditorUtility.tags[j];
                //}

                //newCar.tag = searchTerm;

                newCar.networkObject.SendRpc(PlayerVehicleBehavior.RPC_SET_VEHICLE_I_D, Receivers.AllBuffered, (int)PlayerManager.playerManager.GetPlayerID(i));

                TextDisplay.CarBase = newCar.gameObject;

                //PlayerCarList.Add(newCar.gameObject);
            }

            //GameObject[] tempList = PlayerCarList.ToArray();

            //SendPlayerList();
            //networkObject.SendRpc(RPC_SEND_PLAYER_LIST, Receivers.All, Serializer.GetInstance().Serialize<GameObject[]>(tempList));
        }
        //else
        //{
        //    for (int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
        //    {
        //        string searchTerm = "Player" + (i + 1);
        //        PlayerCarList[i].tag = searchTerm;
        //    }
        //}

        yield return StartCoroutine(Camera.main.GetComponent<CameraFollow>().LoadCamera());
    }

	// Update is called once per frame
	void Update ()
    {
	}

    //public override void SendPlayerList(RpcArgs args)
    //{
    //    PlayerCarList = new List<GameObject>(Serializer.GetInstance().Deserialize<GameObject[]>(args.GetNext<byte[]>()));
    //    Debug.Log("Called");
    //}
}
