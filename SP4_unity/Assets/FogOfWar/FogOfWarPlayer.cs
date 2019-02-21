using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarPlayer : MonoBehaviour
{
    GameObject FogPlane;
    public uint Number;
    private float _fogRad;
    private float StartingFogRad;

    private float temp;

    // Use this for initialization
    void Start()
    {
        StartingFogRad = 20;
       // Number = PlayerManager.playerManager.m_players[PlayerManager.playerManager.GetPlayerIndex()].player_ID;
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
        }
        else
        {
            
            _fogRad = temp - WorldClock._worldTime;
            FindFogPlane().GetComponent<Renderer>().material.SetFloat("FogRadius", _fogRad);
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
