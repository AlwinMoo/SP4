using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound : MonoBehaviour
{
    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

    }

    void Update()
    {
        audioSrc.volume = SFX.SFXvolchanger.audioSrc.volume;
    }
}
