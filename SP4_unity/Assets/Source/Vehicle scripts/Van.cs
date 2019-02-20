using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Van : VehicleBase
{
    public override void Start()
    {
        health = 250;
        this.gameObject.GetComponent<Rigidbody>().mass = 1500;
        mass = this.gameObject.GetComponent<Rigidbody>().mass;

        //transform.Find("MachineGun").gameObject.SetActive(false);
        //transform.Find("FlameThrower").gameObject.SetActive(!transform.Find("MachineGun").gameObject.activeSelf);

        motorForce = 600;
        steerForce = 9000;
        brakeForce = 5 * motorForce;

        fR_Wheel = GameObject.FindWithTag("FR_Collider").GetComponent<WheelCollider>();
        fL_Wheel = GameObject.FindWithTag("FL_Collider").GetComponent<WheelCollider>();
        rR_Wheel = GameObject.FindWithTag("RR_Collider").GetComponent<WheelCollider>();
        rL_Wheel = GameObject.FindWithTag("RL_Collider").GetComponent<WheelCollider>();

        fR_T = GameObject.FindWithTag("FR_Transform").GetComponent<Transform>();
        fL_T = GameObject.FindWithTag("FL_Transform").GetComponent<Transform>();
        rR_T = GameObject.FindWithTag("RR_Transform").GetComponent<Transform>();
        rL_T = GameObject.FindWithTag("RL_Transform").GetComponent<Transform>();

        driveTrain = VehicleBase.DriveTrain.DRIVE_AWD;
        vehicleType = VehicleType.VEH_VAN;

        base.Start();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Zombie collided with player");
            //return;
            Debug.Log(this.gameObject.GetComponent<Rigidbody>().velocity);
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().isKinematic = false;

            m1 = this.GetComponent<Rigidbody>().mass;
            m2 = collision.gameObject.GetComponent<Rigidbody>().mass;
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
