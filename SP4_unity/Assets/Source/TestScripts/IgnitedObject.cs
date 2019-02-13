using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitedObject : MonoBehaviour, Flammable {
	public ParticleSystem fire;
	public ParticleSystem glow;
	public float burnDuration = 3.0f;
	private bool m_burning;
	private float m_countDown;
	// Use this for initialization
	void Start () {
		fire.Stop ();
		glow.Stop ();
		m_countDown = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_burning) // Only if the object is burning still
		{
			m_countDown -= Time.deltaTime;	// Update countdown
			if (m_countDown <= 0.0f) 
			{
				// Stop the burning emission
				fire.Stop ();
				glow.Stop ();
				m_burning = false;	// no longer burning
			}
		}
	}
	public bool Ignited ()
	{
		if (!m_burning) 
		{
			// Only if it wasn't burning at first, call the play function
			m_burning = true;
			fire.Play ();
			glow.Play ();
		}
		m_countDown = burnDuration;	// Reset the countdown
		return true;
	}
}
