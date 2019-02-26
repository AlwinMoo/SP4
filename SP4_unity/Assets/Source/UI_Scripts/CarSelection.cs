﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour {

    public int CARID;

    public GameObject Sedan;
    public GameObject Van;
    public GameObject Monstertruck;


    public void SelectID()
    {
        SliderValue.ID = CARID;
        PlayerManager.playerManager.m_players[PlayerManager.playerManager.GetPlayerIndex()].player_car = SliderValue.ID;
    }

    void Update()
    {
        switch (SliderValue.ID)
        {
            case 0:
                {
                    if(Van != null)
                        Van.SetActive(false);
                    if (Monstertruck != null)
                        Monstertruck.SetActive(false);
                    Sedan.SetActive(true);
                    break;
                }
            case 1:
                {
                    if (Sedan != null)
                        Sedan.SetActive(false);
                    if (Monstertruck != null)
                        Monstertruck.SetActive(false);
                    Van.SetActive(true);
                    break;
                }
            case 2:
                {
                    if(Sedan != null)
                        Sedan.SetActive(false);
                    if (Van != null)
                        Van.SetActive(false);
                    Monstertruck.SetActive(true);
                    break;
                }
        }

    }
}
