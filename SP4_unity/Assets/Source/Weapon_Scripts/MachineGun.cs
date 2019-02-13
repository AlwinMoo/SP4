using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {

	// Reference to object pooler. (Because this will be called at a high rate
	ObjectPooler objectPooler;
	public float fireRate = 0.2f;
	public GameObject flash;

	private float m_countDown = 0.0f;

	private void Start()
	{
		objectPooler = ObjectPooler.Instance;
	}

	void Update() {
		m_countDown -= Time.deltaTime;

		if (Input.GetMouseButton (0) && m_countDown <= 0.0f)
        {
            m_countDown += fireRate;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                objectPooler.SpawnFromPool("HMG_Bullet", transform.position, Quaternion.LookRotation(hit.point - transform.position));
                
                // Play thegunfire light
                flash.GetComponent<HMG_Flash>().StartLight();
            }
		}
		// More accurate firerate if more than 1 shot is fired in a row
		else if (m_countDown <= 0.0f) 
		{
			m_countDown = fireRate;
		}
	}

}