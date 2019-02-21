using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RL_Bullet : MonoBehaviour, IPooledObject
{
    public float bulletForce = 60.0f;
    public const float maxLifeTime = 3.0f;
    private float m_currLifeTime = maxLifeTime;
    public ParticleSystem explosion;
    public ParticleSystem smoke;

    public void OnObjectSpawn()
    {
        m_currLifeTime = maxLifeTime;
        // direction vector for bullet's direction
        Vector3 shootDirection = this.transform.rotation * Vector3.forward;
        // Set bullet force to go towards the shooting direction
        Vector3 force = shootDirection * bulletForce;
        // Apply force to bullet
        GetComponent<Rigidbody>().velocity = force;
        Debug.Log(GetComponent<Rigidbody>().velocity);
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
        ILiveEntity target = col.gameObject.GetComponent<ILiveEntity>();

        if (target != null)
        {
            target.TakeDamage(GlobalDamage.g_RocketDirectDamage, GlobalDamage.DamageTypes.DAMAGE_ROCKET);
            //this.gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(col.collider, this.gameObject.GetComponent<Collider>());
        }

        //TODO: fix rocket detonating on flamethrowers
        ObjectPooler.Instance.SpawnFromPool("RL_Explosion", transform.position, gameObject.transform.rotation);
        var explosionScript = this.gameObject.GetComponent<RL_Explosion>();
        explosionScript.Explosion();
        this.gameObject.SetActive(false);

       
    }
}
