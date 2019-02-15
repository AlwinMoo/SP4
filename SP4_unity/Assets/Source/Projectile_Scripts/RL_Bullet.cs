using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Bullet : MonoBehaviour, IPooledObject
{
    public float bulletForce;
    public const float maxLifeTime = 3.0f;
    private float m_currLifeTime = maxLifeTime;
    public ParticleSystem explosion;
    public ParticleSystem smoke;

    public void OnObjectSpawn()
    {
        var RL_BulletForce = this.gameObject.GetComponent<RocketLauncher>();
        bulletForce = RL_BulletForce.bulletForce;
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

    void OnCollisionEnter (Collision col)
    {
        //TODO: fix rocket detonating on flamethrowers
        ObjectPooler.Instance.SpawnFromPool("RL_Explosion", transform.position, gameObject.transform.rotation);
        var explosionScript = this.gameObject.GetComponent<RL_Explosion>();
        explosionScript.Explosion();
        this.gameObject.SetActive(false);
    }
}
