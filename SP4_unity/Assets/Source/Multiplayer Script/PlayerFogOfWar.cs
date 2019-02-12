using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFogOfWar : MonoBehaviour
{
    public GameObject FogPlane;

	// Use this for initialization
	void Start ()
    {
        FindFogPlane();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FindFogPlane()
    {
        FogPlane = GameObject.Find("FogOfWarPlane");
        Debug.Log("Found FogPlane");
    }
}
