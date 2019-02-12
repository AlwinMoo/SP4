using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour {

    public Slider SFXslider;

    void Start  ()
    {
        SFXslider.value = SFX.SFXvolchanger.audioSrc.volume;
    }

    void Update ()
    {
        SFX.SFXvolchanger.audioSrc.volume = SFXslider.value;
    }
}
