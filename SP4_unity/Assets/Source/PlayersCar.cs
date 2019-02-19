using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class PlayersCar : MonoBehaviour {

    public static int PlayerscarID;

    void Start()
    {
        // This is using data persistance
        //if (File.Exists(Application.persistentDataPath + "/DataFile.dat"))
        //{
        //    BinaryFormatter BF = new BinaryFormatter();
        //    // opens the file
        //    FileStream file = File.Open(Application.persistentDataPath + "/DataFile.dat", FileMode.Open);
        //    Data data = (Data)BF.Deserialize(file);
        //    file.Close();

        //    PlayerscarID = data.SelectedvehicleID;
        //}

        PlayerManager.playerManager.m_players[PlayerManager.playerManager.GetPlayerIndex()].player_car = PlayerscarID;


    }


    void Update () {
        
    }
}
