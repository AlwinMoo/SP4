using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggler : MonoBehaviour {

    public GameObject OpenPanel;
    public GameObject ClosePanel;

    public void PanelOpen() 
    {
        if(OpenPanel != null)
        {
            OpenPanel.SetActive(true);
        }
    }

    public void PanelClose()
    {
        if(ClosePanel != null)
        {
            ClosePanel.SetActive(false);
        }
    }
}
