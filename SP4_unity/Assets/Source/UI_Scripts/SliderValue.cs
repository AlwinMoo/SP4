using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

    public Slider HealthSlider;
    public Slider SpeedSlider;
    public Slider WeightSlider;
    public Text CarName;

    public static int ID;

    void Update ()
    {
        switch (ID)
        {
                case 0:
                {
                    HealthSlider.value = 100;
                    SpeedSlider.value = 7;
                    WeightSlider.value = 1000;
                    CarName.text = "Sedan";
                    
                    break;
                }

            case 1:
                {
                    HealthSlider.value = 250;
                    SpeedSlider.value = 3;
                    WeightSlider.value = 1500;
                    CarName.text = "Van";
                    break;
                }

        }
	}

}


