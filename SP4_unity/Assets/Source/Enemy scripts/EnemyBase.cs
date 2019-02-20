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
                if (target == null)
                    return;
                agent.SetDestination(target.transform.position);
                networkObject.position = target.transform.position;
            }

            m_countDown = 0.0f;
            
            agent.stoppingDistance = 5;

            SerializableVector3[] tempList = { new SerializableVector3(1, 1, 1), new SerializableVector3(2, 2, 2) };
            byte[] bytes = Serializer.GetInstance().Serialize<SerializableVector3[]>(tempList);
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
        SerializableVector3[] temp = Serializer.GetInstance().Deserialize<SerializableVector3[]>(path);
    }
}
