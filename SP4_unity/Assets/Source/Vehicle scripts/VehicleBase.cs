using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class VehicleBase : PlayerVehicleBehavior {

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; } //arbitrary health unit
    public float mass { get; set; }
    public float armour { get; set; }

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

    GameObject aimingRay;

    static Material lineMaterial;

    float maxSteerAngle = 30;

    public enum DriveTrain
    {
        DRIVE_AWD,
        DRIVE_RWD,
        DRIVE_FWD,
    }
    public DriveTrain driveTrain { get; set; }

    public enum VehicleType
    {
        VEH_SEDAN,
        VEH_VAN,
    }
    public VehicleType vehicleType { get; set; }

    public Slider HealthSlider;

    // Use this for initialization
    public virtual void Start()
    {
        HealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        HealthSlider.maxValue = health;
        armour = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // If the gameobject is not owned by the client
        if (!networkObject.IsOwner)
        {
            // assign the gameobject's position to the position assigned on the server
            transform.position = networkObject.position;
            // assign the gameobject's rotation to the rotation assigned on the server
            transform.rotation = networkObject.rotation;

            return;
        }
        rR_Wheel.motorTorque = 0;
        rL_Wheel.motorTorque = 0;

        fR_Wheel.motorTorque = 0;
        fL_Wheel.motorTorque = 0;
        
        HealthSlider.value = health;

        if (Input.GetMouseButton(0))
        {
            //RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, this.transform.position);
            float distToPlane;

            if (plane.Raycast(ray, out distToPlane))
            {
                Vector3 hitPos = ray.GetPoint(distToPlane);

                if (!aimingRay)
                    aimingRay = new GameObject();
                
                aimingRay.transform.position = this.transform.position;

                //CreateLineMaterial();
                if (!aimingRay.GetComponent<LineRenderer>())
                {
                    aimingRay.AddComponent<LineRenderer>();
                }

                LineRenderer aimLine = aimingRay.GetComponent<LineRenderer>();
                aimLine.material = new Material(Shader.Find("Sprites/Default"));

                Color endRed = Color.red;
                endRed.a = 0.3f;
                Color startRed = Color.red;
                startRed.a = 0.6f;
                aimLine.startColor = startRed;
                aimLine.endColor = endRed;
                aimLine.startWidth = 0.15f;
                aimLine.endWidth = aimLine.startWidth;

                aimLine.SetPosition(0, transform.position);
                aimLine.SetPosition(1, hitPos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Destroy(aimingRay);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rL_Wheel.brakeTorque = brakeForce;
            rR_Wheel.brakeTorque = brakeForce;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rL_Wheel.brakeTorque = 0;
            rR_Wheel.brakeTorque = 0;
        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < -3 && Input.GetKeyDown(KeyCode.W))
        {
            rL_Wheel.brakeTorque = brakeForce;
            rR_Wheel.brakeTorque = brakeForce;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rL_Wheel.brakeTorque = 0;
            rR_Wheel.brakeTorque = 0;
        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3 && Input.GetKeyDown(KeyCode.S))
        {
            rL_Wheel.brakeTorque = brakeForce;
            rR_Wheel.brakeTorque = brakeForce;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rL_Wheel.brakeTorque = 0;
            rR_Wheel.brakeTorque = 0;
        }

        // Update the client's position on the server
        networkObject.position = transform.position;
        // Update the client's rotaition on the server
        networkObject.rotation = transform.rotation;
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
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (this.gameObject.GetComponent<VehicleBase>().vehicleType)
            {
                case VehicleType.VEH_SEDAN:
                    collision.gameObject.GetComponent<EnemyBase>().health -= 10;
                    break;
                case VehicleType.VEH_VAN:
                    collision.gameObject.GetComponent<EnemyBase>().health -= 15;
                    break;
                default:
                    break;
            }
        }
    }

    public override void SetVehicleID(RpcArgs args)
    {
        // Check if new car id matches player identity
        if (args.GetNext<int>() == (int)(PlayerManager.playerManager.GetPlayerID((int)PlayerManager.playerManager.GetPlayerIndex())))
        {
            networkObject.TakeOwnership();
        }
        else
            return;

        //TO DO: CAMERA STUFF HERE
    }
}
