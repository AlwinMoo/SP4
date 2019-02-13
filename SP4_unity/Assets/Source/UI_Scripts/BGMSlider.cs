using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour {

    public static BGMSlider BGMslid;
    public Slider BGMslider;

    void Awake()
    {
        BGMslid = this;
    }

    void Start  ()
    {
        BGMslider.value = BGM.BGMvolchanger.audioSrc.volume;
	}
	
	void Update ()
    {
        BGM.BGMvolchanger.audioSrc.volume = BGMslider.value;
    }
}
