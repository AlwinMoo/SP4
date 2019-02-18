using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToggler : MonoBehaviour {

    public Toggle ReadyToggle;

	public void ToggleOnOff()
    {
        if(ReadyToggle.isOn)
        {
            Debug.Log("NotReady");
            ReadyToggle.isOn = false;
        }
        else
        {
            Debug.Log("Ready");
            ReadyToggle.isOn = true;
        }
    }
}
