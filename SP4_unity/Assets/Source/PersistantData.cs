using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistantData : MonoBehaviour
{
    public void Save()
    {
        // Do not use MonoBehaviour class to save
        BinaryFormatter BF = new BinaryFormatter();
        // Application.persistentDataPath(where you are saving to) // "/SaveFile.dat" is the file name
        FileStream file = File.Create(Application.persistentDataPath + "/SoundFile.dat");

        SoundData data = new SoundData();
        data.BGMvolume = BGM.BGMvolchanger.audioSrc.volume;
        data.SFXvolume = SFX.SFXvolchanger.audioSrc.volume;

        Debug.Log("BGMSaveData" + data.BGMvolume);

        // This puts the data in the file BF
        BF.Serialize(file, data);
        // Remember to close file
        file.Close();
    }

    public void Load()
    {
        // Check if the file is there
        if(File.Exists(Application.persistentDataPath + "/SoundFile.dat"))
        {
            BinaryFormatter BF = new BinaryFormatter();
            // opens the file
            FileStream file = File.Open(Application.persistentDataPath + "/SoundFile.dat", FileMode.Open);
            SoundData data = (SoundData)BF.Deserialize(file);
            file.Close();

            BGM.BGMvolchanger.audioSrc.volume = data.BGMvolume;
            SFX.SFXvolchanger.audioSrc.volume = data.SFXvolume;
            BGMSlider.BGMslid.BGMslider.value = data.BGMvolume;
            SFXSlider.SFXSlid.SFXslider.value = data.SFXvolume;


            Debug.Log("BGMLoadedData" + data.BGMvolume);
        }
    }

}

// [Serializable] is needed to tell unity that this class can be saved
[Serializable]
class SoundData
{
    public float SFXvolume;
    public float BGMvolume;
}
