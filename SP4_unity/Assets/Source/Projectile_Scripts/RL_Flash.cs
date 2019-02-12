using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Flash : MonoBehaviour
{
    public float startingIntensity = 10.0f;
    public float flashDuration = 0.2f;

    private float m_countDown;
    private Light m_light;

	void Start ()
    {
        m_light = GetComponent<Light>();
	}
	
    public void InitLight()
    {
        m_countDown = flashDuration;
        m_light.intensity = startingIntensity;
        m_light.enabled = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (m_countDown <= 0.0f)
            return;
        m_countDown -= Time.deltaTime;
        if(m_countDown <= 0.0f)
        {
            m_light.enabled = false;
            return;
        }
        m_light.intensity = startingIntensity * (m_countDown / flashDuration);

	}
}
