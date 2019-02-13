using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    ObjectPooler objectPooler;
    public float fireRate = 2.0f;
    public GameObject flash;
    public bool m_bMouse1State = false;

    private float m_countDown = 0.0f;

	// Use this for initialization
	private void Start ()
    {
        objectPooler = ObjectPooler.Instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_countDown -= Time.deltaTime;
        
        if (Input.GetMouseButton(1) && m_countDown <= 0.0f && !m_bMouse1State)
        {
            m_bMouse1State = true;
            Debug.Log("spawning RL_Bullet");
            objectPooler.SpawnFromPool("RL_Bullet", transform.position, gameObject.transform.rotation);
            m_countDown += fireRate;
           
            flash.GetComponent<RL_Flash>().InitLight();
        }

        else if (m_countDown <= 0.0f && !Input.GetMouseButton(1))
        {
            m_countDown = fireRate;
            m_bMouse1State = false;
        }
    }
}
