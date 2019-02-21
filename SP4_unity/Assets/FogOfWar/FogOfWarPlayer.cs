﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarPlayer : MonoBehaviour
{
    GameObject FogPlane;
    public uint Number;
    private float _fogRad;
    private float _MaxfogRad;
    private float StartingFogRad;
    private float StartingMaxFogRad;

    private float temp;
    private float MaxFogtemp;

    // Use this for initialization
    void Start()
    {
        StartingFogRad = 20;
        StartingMaxFogRad = 0.7f;
        //Number = PlayerManager.playerManager.m_players[PlayerManager.playerManager.GetPlayerIndex()].player_ID;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);

        if(WorldClock._worldTime < 12)
        {
            _fogRad = StartingFogRad + WorldClock._worldTime;
            FindFogPlane().GetComponent<Renderer>().material.SetFloat("FogRadius", _fogRad);
            temp = _fogRad + WorldClock._worldTime;

            _MaxfogRad = StartingMaxFogRad + WorldClock._worldTime * 0.1f;;
            FindFogPlane().GetComponent<Renderer>().material.SetFloat("_FogMaxRadius", _MaxfogRad);
            MaxFogtemp = _MaxfogRad + WorldClock._worldTime * 0.1f;
        }
        else
        {
            
            _fogRad = temp - WorldClock._worldTime;
            FindFogPlane().GetComponent<Renderer>().material.SetFloat("FogRadius", _fogRad);

            _MaxfogRad = MaxFogtemp - WorldClock._worldTime * 0.1f;
            FindFogPlane().GetComponent<Renderer>().material.SetFloat("_FogMaxRadius", _MaxfogRad);
        }

        // This checks if hits the fog of war plane if does creates a hole to the player
        RaycastHit hit;
        if (Physics.Raycast(rayToPlayerPos, out hit, 1000))
        {
            FindFogPlane().GetComponent<Renderer>().material.SetVector("Player" + Number.ToString(), hit.point);
            //Debug.Log("IsFollowing");
        }
    }

    Transform FindFogPlane()
    {
        FogPlane = GameObject.FindWithTag("FogOfWarPlane");
        return FogPlane.transform;
    }

}
