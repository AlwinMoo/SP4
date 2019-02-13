using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour {
    
    public static SFXSlider SFXSlid;
    public Slider SFXslider;

    void Awake()
    {
        SFXSlid = this;
    }
    void Start  ()
    {
        SFXslider.value = SFX.SFXvolchanger.audioSrc.volume;
    }

    void Update ()
    {
        SFX.SFXvolchanger.audioSrc.volume = SFXslider.value;
    }
}
