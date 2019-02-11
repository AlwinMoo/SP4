﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedan : VehicleBase
{
    // Use this for initialization
    public override void Start () {
        health = 100;
        mass = 1;

        motorForce = 1000;
        steerForce = 9000;

        fR_Wheel = GameObject.FindWithTag("FR_Collider").GetComponent<WheelCollider>();
        fL_Wheel = GameObject.FindWithTag("FL_Collider").GetComponent<WheelCollider>();
        rR_Wheel = GameObject.FindWithTag("RR_Collider").GetComponent<WheelCollider>();
        rL_Wheel = GameObject.FindWithTag("RL_Collider").GetComponent<WheelCollider>();

        fR_T = GameObject.FindWithTag("FR_Transform").GetComponent<Transform>();
        fL_T = GameObject.FindWithTag("FL_Transform").GetComponent<Transform>();
        rR_T = GameObject.FindWithTag("RR_Transform").GetComponent<Transform>();
        rL_T = GameObject.FindWithTag("RL_Transform").GetComponent<Transform>();

        driveTrain = VehicleBase.DriveTrain.DRIVE_FWD;
    }
}
