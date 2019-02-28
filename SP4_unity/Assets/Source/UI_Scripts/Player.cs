using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text Name;
    public Text Car;
    public int slotNumber;

	// Update is called once per frame
	void Update () {
        Name.text = PlayerManager.playerManager.GetPlayerName(slotNumber);

        switch (PlayerManager.playerManager.GetPlayerCar(slotNumber))
        {
            case 0:
                {
                    Car.text = "Sedan";
                    break;
                }
            case 1:
                {
                    Car.text = "Van";
                    break;
                }
            case 2:
                {
                    Car.text = "MonsterTruck";
                    break;
                }
        }


	}
}
