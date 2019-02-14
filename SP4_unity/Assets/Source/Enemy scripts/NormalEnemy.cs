using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;

public class NormalEnemy : EnemyBase {

    Rigidbody thisGO;
	// Use this for initialization
	public override void Start ()
    {
        health = 20;
        thisGO = this.gameObject.GetComponent<Rigidbody>();
        thisGO.mass = 20;
        mass = thisGO.mass;
        enemyType = enemytype.ENEMY_NORMAL;
        agent.speed = 3.5f;
	}

    public override void Update()
    {
        base.Update();

        if (health <= 0)
            Destroy(this.gameObject);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (!this.enabled)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            thisGO.isKinematic = false;

            m1 = this.mass;
            m2 = collision.gameObject.GetComponent<VehicleBase>().mass;
            //u1 = this.gameObject.GetComponent<NavMeshAgent>().velocity;
            u1 = thisGO.velocity;
            u2 = collision.gameObject.GetComponent<Rigidbody>().velocity;

            Vector3 N = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;

            //this.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
            thisGO.velocity = u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N;
            collision.gameObject.GetComponent<Rigidbody>().velocity = u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N;
            
        }
    }
}
