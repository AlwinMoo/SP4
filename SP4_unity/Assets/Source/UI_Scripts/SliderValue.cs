using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

    public Slider HealthSlider;
    public Slider SpeedSlider;
    public Slider WeightSlider;

    public static int ID;

    void Update ()
    {
        switch (ID)
        {
            case 1:
            {
                HealthSlider.value = 100;
                SpeedSlider.value = 7;
                WeightSlider.value = 1000;
                break;
            }

            case 2:
                {
                    HealthSlider.value = 250;
                    SpeedSlider.value = 3;
                    WeightSlider.value = 1500;
                    break;
                }

        }
        

	}

}


