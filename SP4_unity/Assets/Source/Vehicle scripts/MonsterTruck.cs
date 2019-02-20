using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTruck : VehicleBase
{    
    public override void Start()
    {
        health = 350;
        this.gameObject.GetComponent<Rigidbody>().mass = 3000;
        mass = this.gameObject.GetComponent<Rigidbody>().mass;

        motorForce = 800;
        steerForce = 9000;
        brakeForce = 5 * motorForce;

    }
	
}
