using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    public static BGM BGMvolchanger;
    public AudioSource audioSrc;

    void Awake()
    {
        audioSrc.volume = 1f;
        if (BGMvolchanger == null)
        {
            DontDestroyOnLoad(gameObject);
            BGMvolchanger = this;
            audioSrc = GetComponent<AudioSource>();
        }
        else if (BGMvolchanger != this)
        {
            Destroy(gameObject);
        }
    }
}
