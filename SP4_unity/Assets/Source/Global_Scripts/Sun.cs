using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour {

    public Text TimeOfDay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, 15f * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        TimeOfDay.text = WorldClock._worldTime.ToString("0");
	}
}
