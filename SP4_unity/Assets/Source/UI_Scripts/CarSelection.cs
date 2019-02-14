using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour {

    public int CARID;

    public GameObject Sedan;
    public GameObject Van;


    public void SelectID()
    {
        SliderValue.ID = CARID;
    }

    void Update()
    {
        switch (SliderValue.ID)
        {
            case 1:
                {
                    if(Van != null)
                    {
                        Van.SetActive(false);
                    }
                    Sedan.SetActive(true);
                    break;
                }
            case 2:
                {
                    if (Sedan != null)
                    {
                        Sedan.SetActive(false);
                    }
                    Van.SetActive(true);
                    break;
                }
        }

    }
}
