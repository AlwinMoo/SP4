using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RL_Explosion : MonoBehaviour
{
    // Gameobject that will trigger the explosion
    private GameObject RL_Bullet;
    // explosion force
    public float power = 100.0f;
    // explosion radius
    public float radius = 2.0f;
    // upward force to send surrounding gameobjects up into the air
    public float upforce = 1.0f;

	// Use this for initialization
	void Start ()
    {
        RL_Bullet = this.gameObject;
    }
	
	// Update is called once per frame
	//void FixedUpdate ()
 //   { 
 //       if(RL_Bullet == enabled)
 //       {
 //           Invoke("Explosion", 1);
 //       }
	//}

    public void Explosion()
    {

        Vector3 explosionPosition = RL_Bullet.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
            ILiveEntity target = rigidbody.gameObject.GetComponent<ILiveEntity>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
            }

            if (target != null && !hit.GetComponent<Rigidbody>())
            {
                rigidbody.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                rigidbody.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                target.TakeDamage(GlobalDamage.g_RocketAOEDamage, GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE);
                this.gameObject.SetActive(false);
            }

            if (rigidbody.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(rigidbody.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
            }
        }


    }

    //void OnCollisionEnter(Collision col)
    //{
    //    ILiveEntity target = col.gameObject.GetComponent<ILiveEntity>();

    //    if (target != null)
    //    {
    //        col.gameObject.GetComponent<NavMeshAgent>().enabled = false;
    //        col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    //        target.TakeDamage(GlobalDamage.g_RocketAOEDamage, GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE);
    //        this.gameObject.SetActive(false);
    //    }

    //    if (col.gameObject.tag == "Player")
    //    {
    //        Physics.IgnoreCollision(col.collider, this.gameObject.GetComponent<Collider>());
    //    }
    //}
}
