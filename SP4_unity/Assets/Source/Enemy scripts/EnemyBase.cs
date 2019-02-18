using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;


using System.Reflection;
using System;

public class EnemyBase : EnemyBehavior {

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; }
    public float mass { get; set; }

    GameObject target;
    public NavMeshAgent agent { get; set; }

    private float m_countDown;

    private float damageTickCD;

    public enum enemytype
    {
        ENEMY_NORMAL,
        ENEMY_TANK,
    } 
    public enemytype enemyType{get; set;}

    [System.Serializable]
    public struct SerializableVector3
    {
        /// <summary>
        /// x component
        /// </summary>
        public float x;

        /// <summary>
        /// y component
        /// </summary>
        public float y;

        /// <summary>
        /// z component
        /// </summary>
        public float z;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="rZ"></param>
        public SerializableVector3(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}]", x, y, z);
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, rValue.z);
        }
    }

    NavMeshPath netPath;

    // Use this for initialization
    public virtual void Awake()
    {
        m_countDown = 0.0f;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        m_countDown += Time.deltaTime;

        //TODO: switch to pooler?
        if (health <= 0)
        {
            if(QuestSystem.ID == 2)
            {
                --QuestSystem.KillsLeft;
            }
            enemy_spawner.enemyList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        if (m_countDown >= 3.0f && GetComponent<NavMeshAgent>().enabled == true && GetComponent<Rigidbody>().isKinematic == true)
        {
            if (!networkObject.IsServer) 
            {
                agent.SetDestination(networkObject.position);
            }
            else
            {
                agent.SetDestination(target.transform.position);
                networkObject.position = target.transform.position;
            }

            m_countDown = 0.0f;
            
            agent.stoppingDistance = 5;

            byte[] bytes = Serializer.GetInstance().Serialize<SerializableVector3>(new SerializableVector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
            networkObject.SendRpc(RPC_GET_PATH, Receivers.All, bytes);
            //Debug.Log("updated destination");
        }
        else if (m_countDown >= 7.0f && GetComponent<NavMeshAgent>().enabled == false && GetComponent<Rigidbody>().isKinematic == false)
        {
            m_countDown = 0.0f;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public virtual void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageTickCD += Time.deltaTime;

            if (damageTickCD >= 2)
            {
                switch (this.gameObject.GetComponent<EnemyBase>().enemyType)
                {
                    case enemytype.ENEMY_NORMAL:
                        collision.gameObject.GetComponent<VehicleBase>().health -= (10 * collision.gameObject.GetComponent<VehicleBase>().armour);
                        break;
                    case enemytype.ENEMY_TANK:
                        collision.gameObject.GetComponent<VehicleBase>().health -= (20 * collision.gameObject.GetComponent<VehicleBase>().armour);
                        break;
                    default:
                        break;
                }
                damageTickCD = 0.0f;
            }
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        //TO DO: CHECK IF NOT OTHER PROJECTILES
        //if (collision.gameObject.name == "HMG_Bullet")
        //{
        //    collision.gameObject.GetComponent<VehicleBase>().health -= 5;
        //}
    }

    public override void GetPath(RpcArgs args)
    {
        GetPath(args.GetNext<byte[]>());
    }

    public void GetPath(byte[] path)
    {
        SerializableVector3 temp = Serializer.GetInstance().Deserialize<SerializableVector3>(path);
    }
}
