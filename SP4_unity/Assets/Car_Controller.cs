using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour {
	private float m_horizonetalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public float motorForce, steerForce, brakeForce;
	public WheelCollider fR_Wheel,fL_Wheel, rR_Wheel, rL_Wheel;
	public Transform fR_T, fL_T, rR_T, rL_T;
	public float maxSteerAngle = 30;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		rR_Wheel.motorTorque = 0;
		rL_Wheel.motorTorque = 0;

		fR_Wheel.motorTorque = 0;
		fL_Wheel.motorTorque = 0;
	}
	public void GetInput()
	{
		m_horizonetalInput = Input.GetAxis ("Horizontal");
		m_verticalInput = Input.GetAxis ("Vertical");
	}
	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizonetalInput;
		// Update the steer angles for the wheels
		fL_Wheel.steerAngle = m_steeringAngle;
		fR_Wheel.steerAngle = m_steeringAngle;
	}
	private void Accelerate()
	{
		// Simulate all wheel drive
		fL_Wheel.motorTorque = m_verticalInput * motorForce;
		fR_Wheel.motorTorque = m_verticalInput * motorForce;
		rL_Wheel.motorTorque = m_verticalInput * motorForce;
		rR_Wheel.motorTorque = m_verticalInput * motorForce;
	}
	private void UpdateWheelPoses()
	{
		// TODO: STEERING WHEEL POS UPDATES HERE
		UpdateWheelPose (fR_Wheel, fR_T);
		UpdateWheelPose (fL_Wheel, fL_T);
		UpdateWheelPose (rR_Wheel, rR_T);
		UpdateWheelPose (rL_Wheel, rL_T);
	}
	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);
		_transform.position = _pos;
		_transform.rotation = _quat;
		_transform.localScale = new Vector3(0.3f, 0.6f, 0.6f);
	}

	private void FixedUpdate()
	{
		GetInput ();
		Steer ();
		Accelerate ();
		UpdateWheelPoses ();
	}

}
