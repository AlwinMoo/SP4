using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class PlayersCar : MonoBehaviour {

    public static int PlayerscarID;
    public GameObject CarBase;

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/DataFile.dat"))
        {
            BinaryFormatter BF = new BinaryFormatter();
            // opens the file
            FileStream file = File.Open(Application.persistentDataPath + "/DataFile.dat", FileMode.Open);
            Data data = (Data)BF.Deserialize(file);
            file.Close();

            PlayerscarID = data.SelectedvehicleID;
        }
    }


    void Update () {
        switch (PlayerscarID)
        {
            case 1:
                {
                    CarBase.GetComponent<Sedan>().enabled = true;
                    CarBase.GetComponent<Van>().enabled = !CarBase.GetComponent<Sedan>().enabled;
                    break;
                }
            case 2:
                {
                    CarBase.GetComponent<Van>().enabled = true;
                    CarBase.GetComponent<Sedan>().enabled = !CarBase.GetComponent<Van>().enabled;
                    break;
                }
        }
    }
}
