using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Bullet : MonoBehaviour, IPooledObject
{
    public const float bulletForce = 30.0f;
    public const float maxLifeTime = 3.0f;
    private float m_currLifeTime = maxLifeTime;

    public void OnObjectSpawn()
    {
        m_currLifeTime = maxLifeTime;
        // direction vector for bullet's direction
        Vector3 shootDirection = this.transform.rotation * Vector3.forward;
        // Set bullet force to go towards the shooting direction
        Vector3 force = shootDirection * bulletForce;
        // Apply force to bullet
        GetComponent<Rigidbody>().velocity = force;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_currLifeTime -= Time.deltaTime;
        if (m_currLifeTime <= 0.0f)
        {
            gameObject.SetActive(false);
        }
	}
}
