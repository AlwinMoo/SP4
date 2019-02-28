using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawning : MonoBehaviour {


    public Text RespawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RespawnTimer.text = "Respawning in: " + (3.0f - GameLogic.respawnTimer).ToString("0");

    }
}
