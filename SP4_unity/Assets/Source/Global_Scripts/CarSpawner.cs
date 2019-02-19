using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    public GameObject SedanPrefab;
    public GameObject VanPrefab;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < PlayerManager.playerManager.m_playerCount; ++i)
        {
            switch (PlayerManager.playerManager.m_players[PlayerManager.playerManager.GetPlayerIndex()].player_car)
            {
                case 0:
                    {
                        Instantiate(SedanPrefab, transform.position, Quaternion.identity);
                    }
                    break;
                case 1:
                    {
                        Instantiate(SedanPrefab, transform.position, Quaternion.identity);
                    }
                    break;
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
