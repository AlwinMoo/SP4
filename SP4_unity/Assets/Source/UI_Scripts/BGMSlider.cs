using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour {

    public Slider BGMslider;

    void Start  ()
    {
        BGMslider.value = BGM.BGMvolchanger.audioSrc.volume;
	}
	
	void Update ()
    {
        BGM.BGMvolchanger.audioSrc.volume = BGMslider.value;
    }
}
