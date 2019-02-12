using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleBase : MonoBehaviour {

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; } //arbitrary health unit
    public float mass { get; set; }

    public float m_horizonetalInput { get; set; }
    public float m_verticalInput { get; set; }
    public float m_steeringAngle { get; set; }

    public float motorForce { get; set; }
    public float steerForce { get; set; }
    public float brakeForce { get; set; }

    public WheelCollider fR_Wheel { get; set; }
    public WheelCollider fL_Wheel { get; set; }
    public WheelCollider rR_Wheel { get; set; }
    public WheelCollider rL_Wheel { get; set; }
    public Transform fR_T { get; set; }
    public Transform fL_T { get; set; }
    public Transform rR_T { get; set; }
    public Transform rL_T { get; set; }

    float maxSteerAngle = 30;

    public enum DriveTrain
    {
        DRIVE_AWD,
        DRIVE_RWD,
        DRIVE_FWD,
    }
    public DriveTrain driveTrain { get; set; }

    //public Slider HealthSlider;

    // Use this for initialization
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        rR_Wheel.motorTorque = 0;
        rL_Wheel.motorTorque = 0;

        fR_Wheel.motorTorque = 0;
        fL_Wheel.motorTorque = 0;

        //HealthSlider.value = health;
    }
    public virtual void GetInput()
    {
        m_horizonetalInput = Input.GetAxis("Horizontal");
        m_verticalInput = -Input.GetAxis("Vertical");
    }
    public virtual void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizonetalInput;
        // Update the steer angles for the wheels
        fL_Wheel.steerAngle = m_steeringAngle;
        fR_Wheel.steerAngle = m_steeringAngle;
    }
    public virtual void Accelerate()
    {
        // Simulate all wheel drive
        switch (driveTrain)
        {
            case DriveTrain.DRIVE_AWD:
                { 
                    fL_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    fR_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    rL_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    rR_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    break;
                }
            case DriveTrain.DRIVE_RWD:
                {
                    rL_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    rR_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    break;
                }
            case DriveTrain.DRIVE_FWD:
                {
                    fL_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    fR_Wheel.motorTorque = m_verticalInput * motorForce / (mass * 0.001f);
                    break;
                }
        }

    }
    public virtual void UpdateWheelPoses()
    {
        // TODO: STEERING WHEEL POS UPDATES HERE
        UpdateWheelPose(fR_Wheel, fR_T);
        UpdateWheelPose(fL_Wheel, fL_T);
        UpdateWheelPose(rR_Wheel, rR_T);
        UpdateWheelPose(rL_Wheel, rL_T);
    }
    public virtual void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
        _transform.localScale = new Vector3(0.3f, 0.6f, 0.6f);
    }

    public virtual void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        Debug.Log(m_verticalInput);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().health -= 10;
        }
    }
}
