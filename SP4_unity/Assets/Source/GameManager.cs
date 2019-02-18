using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < 4; ++i)
        {
            Vector3 randpos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
            var newenemy = NetworkManager.Instance.InstantiatePlayerVehicle(PlayerManager.playerManager.m_players[i].player_car, randpos, transform.rotation, true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
