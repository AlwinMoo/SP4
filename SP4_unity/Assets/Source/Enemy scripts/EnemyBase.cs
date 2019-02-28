using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using System.Linq;

public class EnemyBase : EnemyBehavior, Flammable
{

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; }
    public float mass { get; set; }
	public float damageTakenTickRate = 0.3f;

    GameObject target;
    public NavMeshAgent agent { get; set; }

    private float m_countDown;
	private float m_damageTakenCD;
	private float m_damageTaken;
    private float damageTickCD;

    public enum enemytype
    {
        ENEMY_NORMAL,
        ENEMY_TANK,
		ENEMY_SPIDER
    } 
    public enemytype enemyType{get; set;}

    public bool m_burning { get; set; }

    // Use this for initialization
    public virtual void Awake()
    {
        m_countDown = 0.0f;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player0");
    }

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        m_countDown += Time.deltaTime;
        
        if (m_countDown >= 3.0f && GetComponent<NavMeshAgent>().enabled == true && GetComponent<Rigidbody>().isKinematic == true)
        {
            if (!networkObject.IsOwner)
            {
                agent.SetDestination(networkObject.position);

                //transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;
            }
            else
            {
                if (target == null)
                    return;
                agent.SetDestination(target.transform.position);
            //TO DO SET NEAREST AS TARGET
                networkObject.position = target.transform.position;
                networkObject.rotation = transform.rotation;
            }

            if(QuestSystem.QuestID == 1)
            {
                if(QuestSystem.theObj != null)
                    target = QuestSystem.theObj;
            }

            m_countDown = 0.0f;
            
            agent.stoppingDistance = 8;
        }
        else if (m_countDown >= 7.0f && GetComponent<NavMeshAgent>().enabled == false && GetComponent<Rigidbody>().isKinematic == false)
        {
            m_countDown = 0.0f;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        if (this.transform.position.y >= -3f)
            this.transform.position = new Vector3(this.transform.position.x, -3f, this.transform.position.z);
    }

    public virtual void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<VehicleBase>() != null)
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
				    case enemytype.ENEMY_SPIDER:
				    	collision.gameObject.GetComponent<VehicleBase>().health -= (15 * collision.gameObject.GetComponent<VehicleBase>().armour);
				    	break;
                    default:
                        break;
                }
                damageTickCD = 0.0f;

                collision.gameObject.GetComponentInChildren<Healthbar>().hpBar.fillAmount = collision.gameObject.GetComponent<VehicleBase>().health / collision.gameObject.GetComponent<VehicleBase>().maxHealth;

            }
        }
        else if (collision.gameObject.CompareTag("Objective"))
        {
            if (damageTickCD >= 2)
            {
                
                damageTickCD = 0.0f;

                collision.gameObject.GetComponent<ObjectiveObject>().health -= 5;

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

	private void EnemyDeath()
	{
		networkObject.Destroy();
	}

    public override void SendDeath(RpcArgs args)
    {
        networkObject.Destroy();
    }

	public override void Stagger(RpcArgs args)
	{
	}

    public virtual bool Ignited() { return false; }

	public virtual void TakeTickDamage(float _damage)
	{
		m_damageTaken += _damage;
	}

	private void UnloadTickDamage()
	{
		health -= m_damageTaken;
		m_damageTaken = 0.0f;
	}

    public override void SendOnFire(RpcArgs args)
    {
        // Play the animation if it hasn't been played
        Ignited();
    }

	public override void TakeDamage(RpcArgs args)
	{
		float damage = args.GetNext<float> ();
		this.health -= damage;
		if (health < 0.0f)
			EnemyDeath();
	}
	// For compatibility with animations
	public virtual void CheckAlive()
	{
		if (health <= 0)
			EnemyDeath ();
	}
}
