using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobby : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    private int ReadyCount;


    // Use this for initialization
    void Start () {
        ReadyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If player Joins Assign the gameObject to the player
        if (Player1.GetComponent<Toggle>().isOn)
        {
            ++ReadyCount;
        }
        if (Player2.GetComponent<Toggle>().isOn)
        {
            ++ReadyCount;
        }
        if (Player3.GetComponent<Toggle>().isOn)
        {
            ++ReadyCount;
        }
        if (Player4.GetComponent<Toggle>().isOn)
        {
            ++ReadyCount;
        }
    }

    public void PlayerJoin()
    {
        Player1.SetActive(true);
    }
}
