using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {

    public static SFX SFXvolchanger;
    public AudioSource audioSrc;

    void Awake()
    {
        audioSrc.volume = 1f;
        if (SFXvolchanger == null)
        {
            DontDestroyOnLoad(gameObject);
            SFXvolchanger = this;
            audioSrc = GetComponent<AudioSource>();
        }
        else if (SFXvolchanger != this)
        {
            Destroy(gameObject);
        }
    }
}
