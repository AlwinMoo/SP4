using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
            }

        }


    }
}
