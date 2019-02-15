using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistantData : MonoBehaviour
{
    // You Can Just Save Everything here
    public void Save()
    {
        // Do not use MonoBehaviour class to save
        BinaryFormatter BF = new BinaryFormatter();
        // Application.persistentDataPath(where you are saving to) // "/SaveFile.dat" is the file name
        FileStream file = File.Create(Application.persistentDataPath + "/DataFile.dat");

        Data data = new Data();
        data.BGMvolume = BGM.BGMvolchanger.audioSrc.volume;
        data.SFXvolume = SFX.SFXvolchanger.audioSrc.volume;
        data.SelectedvehicleID = SliderValue.ID;

        // This puts the data in the file BF
        BF.Serialize(file, data);
        // Remember to close file
        file.Close();
    }

    // Can just copy this function
    public void Load()
    {
        // Check if the file is there
        if(File.Exists(Application.persistentDataPath + "/DataFile.dat"))
        {
            BinaryFormatter BF = new BinaryFormatter();
            // opens the file
            FileStream file = File.Open(Application.persistentDataPath + "/DataFile.dat", FileMode.Open);
            Data data = (Data)BF.Deserialize(file);
            file.Close();

            // Copy till here then call which value u want from the save file 

            BGM.BGMvolchanger.audioSrc.volume = data.BGMvolume;
            SFX.SFXvolchanger.audioSrc.volume = data.SFXvolume;
            BGMSlider.BGMslid.BGMslider.value = data.BGMvolume;
            SFXSlider.SFXSlid.SFXslider.value = data.SFXvolume;
        }
    }

}

// [Serializable] is needed to tell unity that this class can be saved
// Just make a variable of the data u want to save
[Serializable]
class Data
{
    public float SFXvolume;
    public float BGMvolume;
    public int SelectedvehicleID;
    public int killCount;
}
