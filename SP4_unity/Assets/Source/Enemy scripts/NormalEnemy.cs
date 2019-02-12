
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : EnemyBase {

	// Use this for initialization
	public override void Start ()
    {
        health = 10;
        mass = 30;
	}

    public override void OnCollisionEnter(Collision collision)
    {
        //float momentum = 0.0f;

        if (collision.gameObject.CompareTag("Player"))
        {
            //momentum = collision.gameObject.GetComponent<VehicleBase>().mass * collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;


            //Debug.Log("Zombie collided with player");
            //return;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;

            m1 = this.mass;
            m2 = collision.gameObject.GetComponent<VehicleBase>().mass * 1000;
            //u1 = this.gameObject.GetComponent<NavMeshAgent>().velocity;
            u1 = this.gameObject.GetComponent<Rigidbody>().velocity;
            u2 = collision.gameObject.GetComponent<Rigidbody>().velocity;

            Vector3 N = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;

            this.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
            //this.gameObject.GetComponent<Rigidbody>().velocity  = u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N;
            //collision.gameObject.GetComponent<Rigidbody>().velocity = u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N;
        }
    }
}
