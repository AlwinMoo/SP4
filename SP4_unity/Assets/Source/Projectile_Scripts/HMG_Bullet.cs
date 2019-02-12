﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMG_Bullet : MonoBehaviour, IPooledObject 
{
	public const float bulletForce = 100.0f;
	public const float maxLifeTime = 3.0f;
	private float m_currLifeTime = maxLifeTime;

	// Use this for initialization
	public void OnObjectSpawn()
	{
		m_currLifeTime = maxLifeTime;
		// Get the view to find which direction the bullet goes
		Vector3 view = this.transform.rotation * Vector3.forward;
		// Set the force to be towards the view
		Vector3 force = view * bulletForce;
		// Apply force
		GetComponent<Rigidbody>().velocity = force;
		Debug.Log ("shooting in " + this.transform.rotation);
	}

	void Update()
	{
		m_currLifeTime -= Time.deltaTime;
		if (m_currLifeTime <= 0.0f) {
			gameObject.SetActive (false);
		}
	}
}