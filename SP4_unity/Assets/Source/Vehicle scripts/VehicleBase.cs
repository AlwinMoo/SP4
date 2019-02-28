using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.Events;
using System;

public class VehicleBase : PlayerVehicleBehavior {

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; } //arbitrary health unit
    public float maxHealth { get; set; }
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
    bool cancelShoot;

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
        VEH_MONSTER_TRUCK,
    }
    public VehicleType vehicleType { get; set; }

    public Slider HealthSlider;

    public Vector3 parentDir;

    // Use this for initialization
    public virtual void Start()
    {
        maxHealth = health;
        HealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        HealthSlider.maxValue = maxHealth;
        armour = 1;

        if (networkObject.IsServer)
        {
			// note: toowner
			this.gameObject.tag = "Player" + networkObject.Owner.NetworkId;
			networkObject.SendRpc (RPC_SEND_TAG, Receivers.All, this.gameObject.tag);
		}
		else if (networkObject.IsOwner) 
		{
			this.gameObject.tag = "Player" + networkObject.Owner.NetworkId;
		}
        StartCoroutine(Camera.main.GetComponent<CameraFollow>().LoadCamera());

        networkObject.isActive = this.gameObject.activeSelf;

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

            parentDir = networkObject.WeaponRotation;

            gameObject.GetComponent<VehicleBase>().SetComponentActive(networkObject.isActive);

            return;
        }

        rR_Wheel.motorTorque = 0;
        rL_Wheel.motorTorque = 0;

        fR_Wheel.motorTorque = 0;
        fL_Wheel.motorTorque = 0;
        
        HealthSlider.value = health;

        if (Input.GetMouseButton(0) && (this.gameObject.GetComponent(typeof(Collider)) as Collider) != false)
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

                Vector3 dir = hitPos - transform.position;
                dir.y = 0;

                parentDir = dir;
                networkObject.WeaponRotation = dir;

                cancelShoot = true;
                networkObject.SendRpc(RPC_TRIGGER_SHOOT, Receivers.All, (int)PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID, true);
            }
        }

        if (Input.GetMouseButtonUp(0) && cancelShoot != false)
        {
            cancelShoot = false;
            networkObject.SendRpc(RPC_TRIGGER_SHOOT, Receivers.All, (int)PlayerManager.playerManager.m_players[(int)PlayerManager.playerManager.GetPlayerIndex()].player_ID, false);
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }

        // Update the client's position on the server
        networkObject.position = transform.position;
        // Update the client's rotaition on the server
        networkObject.rotation = transform.rotation;

        if (health <= 0)
        {
            transform.position = new Vector3(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            TextDisplay.Isdead();

            SetComponentActive(false);

            networkObject.isActive = false;
        }
        else
        {
            networkObject.isActive = true;
        }

        if (this.transform.position.y >= -3f)
            this.transform.position = new Vector3(this.transform.position .x, -3f, this.transform.position.z);
    }
    public virtual void GetInput()
    {
        m_horizonetalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }
    public virtual void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizonetalInput;
        // Update the steer angles for the wheels
        fL_Wheel.steerAngle = m_steeringAngle;
        fR_Wheel.steerAngle = m_steeringAngle;
    }

    public virtual void SetComponentActive(bool _status)
    {
        ///Set children
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(_status);
        }

        ///Set monobehavior components
        MonoBehaviour[] component = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in component)
        {
            c.enabled = _status;
        }

        foreach (var c in this.gameObject.GetComponents(typeof(Collider)))
        {
            (c as Collider).enabled = _status;
        }

        (this.gameObject.GetComponent(typeof(Renderer)) as Renderer).enabled = _status;

        GetComponent<VehicleBase>().enabled = true;
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
    
    public virtual void InitWheelScale(Transform _transform, Vector3 _wheelScale)
    {
        _transform.localScale = _wheelScale;
    }

    public virtual void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
        //_transform.localScale = new Vector3(0.3f, 0.6f, 0.6f);
    }

    public virtual void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

	/// <summary>
	/// Receives damage when the player enters collision with an enemy. Different vehicle types take different damage amounts
	/// </summary>
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
                case VehicleType.VEH_MONSTER_TRUCK:
                    collision.gameObject.GetComponent<EnemyBase>().health -= 20;
                    break;
                default:
                    break;
            }
        }
    }
	/// <summary>
	/// Triggers fire when a client shoots
	/// </summary>
    public override void triggerShoot(RpcArgs args)
    {
        int ShooterID = args.GetNext<int>();
        bool ShootingStatus = args.GetNext<bool>();

        if (this.gameObject.tag != "Player" + ShooterID)
        {
			return;
		}

        switch (this.gameObject.GetComponent<VehicleBase>().vehicleType)
        {
            case VehicleType.VEH_SEDAN:
                {
                    EventManager.TriggerEvent("MGShoot", this.gameObject.tag);
                    break;
                }
            case VehicleType.VEH_VAN:
                {
                    if (!ShootingStatus)
                    {
                        EventManager.TriggerEvent("CancelFire", this.gameObject.tag);
                        break;
                    }

                    EventManager.TriggerEvent("FireShoot", this.gameObject.tag);
                    break;
                }
            case VehicleType.VEH_MONSTER_TRUCK:
                {
                    EventManager.TriggerEvent("RocketShoot", this.gameObject.tag);
                    break;
                }
            default:
                break;
        }
        
    }

    public override void SendTag(RpcArgs args)
    {
        string SetName = args.GetNext<string>();
		Debug.Log ("Found name of: " + SetName + ". Checking with " + this.gameObject.tag);
        if (this.gameObject.tag == "Player")
        {
            //if (GameObject.FindGameObjectWithTag(SetName) != null)
            //    return;
			Debug.Log (this.gameObject.tag + " set to " + SetName);

            this.gameObject.tag = SetName;
        }
    }
}
