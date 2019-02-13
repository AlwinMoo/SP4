using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    ObjectPooler objectPooler;
    public float fireRate = 2.0f;
    public GameObject flash;

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

        if (Input.GetMouseButton(1) && m_countDown <= 0.0f)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                objectPooler.SpawnFromPool("HMG_Bullet", transform.position, Quaternion.LookRotation(hit.point - transform.position));

                // Play thegunfire light
                flash.GetComponent<RL_Flash>().InitLight();
            }

            m_countDown += fireRate;
        }

        else if (m_countDown <= 0.0f)
        {
            m_countDown = fireRate;
        }
    }
}
