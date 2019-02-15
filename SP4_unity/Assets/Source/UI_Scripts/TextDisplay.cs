using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    //public Text HighScore;
    public Text Speed;
    public Text Armour;
    public Text Health;
    public GameObject CarBase;

    // Update is called once per frame
    void Update () {
        //HighScore.text = playerKills.ToString();
        Health.text = "Health: " + CarBase.GetComponent<VehicleBase>().HealthSlider.value.ToString();
        Speed.text = "Speed: " + CarBase.GetComponent<Rigidbody>().velocity.magnitude.ToString("0");
        Armour.text = "Armour: " + CarBase.GetComponent<VehicleBase>().armour.ToString();
	}
}
