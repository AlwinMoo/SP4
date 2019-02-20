using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using System.Linq;

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

    public bool m_burning { get; set; }

    // Use this for initialization
    public virtual void Awake()
    {
        m_countDown = 0.0f;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player1");
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
            if (QuestSystem.ID == 2)
            {
                --QuestSystem.KillsLeft;
            }
            
            for (int i = 0; i < enemy_spawner.enemyList.Count; ++i)
            {
                if (enemy_spawner.enemyList[i].gameObject == this.gameObject)
                {
                    GameObject.Find("Global").GetComponent<enemy_spawner>().DestroyEnemy(i);
                }
            }
        }

        if (m_countDown >= 3.0f && GetComponent<NavMeshAgent>().enabled == true && GetComponent<Rigidbody>().isKinematic == true)
        {
            //if (!networkObject.IsServer) 
            //{
            //    agent.SetDestination(networkObject.position);
            //}
            //else
            //{
                if (target == null)
                    return;
                agent.SetDestination(target.transform.position);
            //TO DO SET NEAREST AS TARGET
            //    networkObject.position = target.transform.position;
            //}

            m_countDown = 0.0f;
            
            agent.stoppingDistance = 5;
            
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

    public override void SendDeath(RpcArgs args)
    {
        int count = args.GetNext<int>();

        if (count < enemy_spawner.enemyList.Count)
        {
            if (enemy_spawner.enemyList[count] != null)
            {
                Destroy(enemy_spawner.enemyList[count].gameObject);
            }
        }
    }
}
