using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class GameLogic : MonoBehaviour
{
    public static Transform serverTransform;

    // Use this for initialization
    void Start ()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        if (PlayerManager.playerManager.networkObject.IsServer)
        {
            for (int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
            {
                Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                var newCar = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[i].player_car, randpos, transform.rotation, true);

                if (PlayerManager.playerManager.m_players[i].player_ID == 0)
                {
                    newCar.gameObject.AddComponent<Camera>();
                    newCar.gameObject.GetComponent<Camera>().gameObject.AddComponent<CameraFollow>();
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
