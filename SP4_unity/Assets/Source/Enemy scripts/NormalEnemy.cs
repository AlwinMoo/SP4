
using UnityEngine;

public class NormalEnemy : EnemyBase {

	// Use this for initialization
	public override void Start ()
    {
        health = 10;
        mass = 30;
	}

    public override void OnCollisionEnter(Collision collision)
    {
        float momentum = 0.0f;

        if (collision.gameObject.CompareTag("Player"))
        {
            momentum = collision.gameObject.GetComponent<VehicleBase>().mass * collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }
}
