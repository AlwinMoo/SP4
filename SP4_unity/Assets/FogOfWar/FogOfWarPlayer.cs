using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarPlayer : MonoBehaviour {

    public Transform FogOfWarPlane;
    public int Number;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);

        RaycastHit hit;
        if(Physics.Raycast(rayToPlayerPos, out hit, 1000))
        {
            FogOfWarPlane.GetComponent<Renderer>().material.SetVector("Player" + Number.ToString(), hit.point);
        }
	}
}
