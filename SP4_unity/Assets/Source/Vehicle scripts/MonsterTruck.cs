using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTruck : VehicleBase
{
    public override void Start()
    {
        health = 200;
        maxHealth = 200;
        this.gameObject.GetComponent<Rigidbody>().mass = 1250;
        mass = this.gameObject.GetComponent<Rigidbody>().mass;
        
        motorForce = 5000;
        steerForce = 9000;
        brakeForce = 5 * motorForce;

        fR_Wheel = GameObject.FindWithTag("FR_Collider").GetComponent<WheelCollider>();
        fL_Wheel = GameObject.FindWithTag("FL_Collider").GetComponent<WheelCollider>();
        rR_Wheel = GameObject.FindWithTag("RR_Collider").GetComponent<WheelCollider>();
        rL_Wheel = GameObject.FindWithTag("RL_Collider").GetComponent<WheelCollider>();

        fR_T = GameObject.FindWithTag("FR_Transform").GetComponent<Transform>();
        InitWheelScale(fR_T.transform, new Vector3(1f, 3.5f, 4f));
       
        fL_T = GameObject.FindWithTag("FL_Transform").GetComponent<Transform>();
        InitWheelScale(fL_T.GetComponent<Transform>(), new Vector3(1f, 3.5f, 4f));
        
        rR_T = GameObject.FindWithTag("RR_Transform").GetComponent<Transform>();
        InitWheelScale(rR_T.GetComponent<Transform>(), new Vector3(1f, 3.5f, 4f));

        rL_T = GameObject.FindWithTag("RL_Transform").GetComponent<Transform>();
        InitWheelScale(rL_T.GetComponent<Transform>(), new Vector3(1f, 3.5f, 4f));

        driveTrain = DriveTrain.DRIVE_AWD;
        vehicleType = VehicleType.VEH_MONSTER_TRUCK;
        
        base.Start();
    }
    
    /// <summary>
    /// Do momentum transfer when collided
    /// </summary>
    /// <param name="collision"> Enemies </param>
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().isKinematic = false;

            m1 = mass;
            m2 = collision.gameObject.GetComponent<Rigidbody>().mass;
            u1 = this.gameObject.GetComponent<Rigidbody>().velocity;
            u2 = collision.gameObject.GetComponent<Rigidbody>().velocity;

            Vector3 N = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;

            this.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
        }
    }
}
