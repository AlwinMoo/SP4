using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    //public Text HighScore;
    public Text Speed;
    public GameObject CarBase;

    // Update is called once per frame
    void Update () {
        //HighScore.text = playerKills.ToString();
        switch (PlayersCar.PlayerscarID)
        {
            case 1:
                {
                    Speed.text = CarBase.GetComponent<Sedan>().u1.ToString("0");
                    break;
                }
            case 2:
                {
                    Speed.text = CarBase.GetComponent<Van>().u1.ToString("0");
                    break;
                }
        }
	}
}
