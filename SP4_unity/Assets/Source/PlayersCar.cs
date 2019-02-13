using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCar : MonoBehaviour {

    public static int PlayerscarID;
    GameObject sedan;
    GameObject van;

	void Update () {

        switch (PlayerscarID)
        {
            case 1:
                {
                    if(van != null)
                    {
                        van.SetActive(true);
                    }
                    sedan.SetActive(true);
                    break;
                }
            case 2:
                {
                    if (sedan != null)
                    {
                        sedan.SetActive(true);
                    }
                    van.SetActive(true);
                    break;
                }
        }
    }
}
