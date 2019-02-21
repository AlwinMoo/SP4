using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobby : MonoBehaviour {

    public GameObject PlayerPanel1;
    public GameObject PlayerPanel2;
    public GameObject PlayerPanel3;
    public GameObject PlayerPanel4;

    public GameObject EmptyPanel1;
    public GameObject EmptyPanel2;
    public GameObject EmptyPanel3;
    public GameObject EmptyPanel4;

    private int ReadyCount;


    // Use this for initialization
    void Start () {
        ReadyCount = 0;
        Debug.Log(PlayerManager.playerManager.GetPlayerCount());
    }

    // Update is called once per frame
    void Update()
    {

        // TODO If a player leaves then make a space to set panel to the player index instead of the playercount


        for(int i = 0; i < 4; ++i)
        {
            if(PlayerManager.playerManager.m_players[i].player_slot_empty == false)
            {
                switch (i)
                {
                    case 0:
                        {
                            EmptyPanel1.SetActive(false);
                            PlayerPanel1.SetActive(true);
                        }
                        break;
                    case 1:
                        {
                            EmptyPanel2.SetActive(false);
                            PlayerPanel2.SetActive(true);
                        }
                        break;
                    case 2:
                        {
                            EmptyPanel3.SetActive(false);
                            PlayerPanel3.SetActive(true);
                        }
                        break;
                    case 3:
                        {
                            EmptyPanel4.SetActive(false);
                            PlayerPanel4.SetActive(true);
                        }
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        {
                            EmptyPanel1.SetActive(true);
                            PlayerPanel1.SetActive(false);
                        }
                        break;
                    case 1:
                        {
                            EmptyPanel2.SetActive(true);
                            PlayerPanel2.SetActive(false);

                        }
                        break;
                    case 2:
                        {
                            EmptyPanel3.SetActive(true);
                            PlayerPanel3.SetActive(false);

                        }
                        break;
                    case 3:
                        {
                            EmptyPanel4.SetActive(true);
                            PlayerPanel4.SetActive(false);
                        }
                        break;
                }
            }
        }
    }
}
