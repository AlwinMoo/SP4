using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobby : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;


    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If player Joins Assign the gameObject to the player
    }

    public void PlayerJoin()
    {
        Player1.SetActive(true);
    }
}
