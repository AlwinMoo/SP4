using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCar : VehicleBase{

	// Use this for initialization
	void Start () {
		health = 250;
		this.gameObject.GetComponent<Rigidbody>().mass = 1500;
		mass = this.gameObject.GetComponent<Rigidbody>().mass;

		transform.Find("MachineGun").gameObject.SetActive(false);
		transform.Find("FlameThrower").gameObject.SetActive(!transform.Find("MachineGun").gameObject.activeSelf);

		motorForce = 600;
		steerForce = 9000;
		brakeForce = 5 * motorForce;

		fR_Wheel = GameObject.FindWithTag("FR_Collider").GetComponent<WheelCollider>();
		fL_Wheel = GameObject.FindWithTag("FL_Collider").GetComponent<WheelCollider>();
		rR_Wheel = GameObject.FindWithTag("RR_Collider").GetComponent<WheelCollider>();
		rL_Wheel = GameObject.FindWithTag("RL_Collider").GetComponent<WheelCollider>();

		fR_T = GameObject.FindWithTag("FR_Transform").GetComponent<Transform>();
		fL_T = GameObject.FindWithTag("FL_Transform").GetComponent<Transform>();
		rR_T = GameObject.FindWithTag("RR_Transform").GetComponent<Transform>();
		rL_T = GameObject.FindWithTag("RL_Transform").GetComponent<Transform>();

		driveTrain = VehicleBase.DriveTrain.DRIVE_AWD;
		vehicleType = VehicleType.VEH_VAN;

		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
