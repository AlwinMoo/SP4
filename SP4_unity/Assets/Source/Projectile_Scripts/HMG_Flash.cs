using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMG_Flash : MonoBehaviour {

	public float startIntensity = 10.0f;
	public float duration = 0.3f;

	private float m_countDown;
	private Light m_lt;

	void Start()
	{
		m_lt = GetComponent<Light> ();
	}
	// Use this for initialization
	public void StartLight()
	{
		m_countDown = duration;
		/// Set Light Intensity
		m_lt.intensity = startIntensity;
		m_lt.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_countDown <= 0.0f)
			return;
		m_countDown -= Time.deltaTime;
		if (m_countDown <= 0.0f)
		{
			m_lt.enabled = false;
			return;
		}
		m_lt.intensity = startIntensity * (m_countDown / duration);
	}

}
