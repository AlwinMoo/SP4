using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BeardedManStudios.Forge.Networking;

public class SpiderEnemy :  EnemyBase, ILiveEntity {
	public ParticleSystem fire;
	public ParticleSystem glow;
	//TODO: PUT THIS VARIABLE INTO BASE INSTEAD
	public const float burnDuration = 3.0f;
	//TODO: PUT THIS VARIABLE INTO BASE INSTEAD
	public const float maxHealth = 200;
	public const float poise = 50;
	//private bool m_burning;
	private float m_countDownSpider;
	private Animator anim;

	// Hashes for calling animations
	private int m_;
	Rigidbody thisGO;
	// Use this for initialization
	void Start () {
		health = maxHealth;

		thisGO = this.gameObject.GetComponent<Rigidbody>();
		thisGO.mass = 100;
		mass = thisGO.mass;

		enemyType = enemytype.ENEMY_SPIDER;
		anim = GetComponent<Animator> ();
		agent.speed = 1.5f;
		m_burning = false;
		m_countDownSpider = 0.0f;
		fire.Stop ();
		glow.Stop ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();

		if (networkObject.IsServer && m_burning)
		{
			m_countDownSpider -= Time.deltaTime;
			float damage;
			if (m_countDownSpider <= 0.0f) 
			{
				m_burning = false;

				// Set damage to be more accurate
				damage = GlobalDamage.g_fireDamageTickRatio * (Time.deltaTime + m_countDownSpider) * maxHealth;
				fire.Stop ();
				glow.Stop ();
			}
			else
				damage = GlobalDamage.g_fireDamageTickRatio * Time.deltaTime * maxHealth;
			// Apply tick damage based on time delta
			TakeDamage(damage, GlobalDamage.DamageTypes.DAMAGE_FIRE_NORMAL);
		}
	}

	public override void OnCollisionEnter(Collision collision)
	{
		if (!this.enabled)
			return;

		if (collision.gameObject.CompareTag("Player"))
		{
			GetComponent<NavMeshAgent>().enabled = false;
			thisGO.isKinematic = false;

			m1 = this.mass;
			m2 = collision.gameObject.GetComponent<VehicleBase>().mass;
			//u1 = this.gameObject.GetComponent<NavMeshAgent>().velocity;
			u1 = thisGO.velocity;
			u2 = collision.gameObject.GetComponent<Rigidbody>().velocity;

			Vector3 N = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;

			//this.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
			//collision.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
			//this.gameObject.GetComponent<Rigidbody>().velocity = u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N;
			//collision.gameObject.GetComponent<Rigidbody>().velocity = u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N;
			collision.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
			this.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
		}
	}

	public bool TakeDamage(float _damage, GlobalDamage.DamageTypes _type)
	{
		if (!this.enabled)
			return false;

		if (networkObject.IsServer) {
			switch (_type) {
			case GlobalDamage.DamageTypes.DAMAGE_BALLISTIC_SMALL:
				health -= _damage;
				break;
			case GlobalDamage.DamageTypes.DAMAGE_FIRE_NORMAL:
				health -= _damage;
				break;
			case GlobalDamage.DamageTypes.DAMAGE_FIRE_TICK:
				health -= _damage;
				break;
			case GlobalDamage.DamageTypes.DAMAGE_ROCKET:
				health -= _damage;
				break;
			case GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE:
				health -= _damage;
				break;
			}
		} 
		else 
		{
			switch (_type)
			{
			case GlobalDamage.DamageTypes.DAMAGE_BALLISTIC_SMALL:
				TakeTickDamage(_damage);
				break;
			case GlobalDamage.DamageTypes.DAMAGE_FIRE_NORMAL:
				TakeTickDamage(_damage);
				break;
			case GlobalDamage.DamageTypes.DAMAGE_FIRE_TICK:
				TakeTickDamage(_damage);
				break;
			case GlobalDamage.DamageTypes.DAMAGE_ROCKET:
				TakeTickDamage(_damage);
				break;
			case GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE:
				TakeTickDamage(_damage);
				break;
			}
		}
		return true;
	}

	public override bool Ignited()
	{
		// If not already burning
		if (!m_burning) 
		{
			// Reset the countdown and play the fire animation
			m_burning = true;
			m_countDownSpider = burnDuration;
			glow.Play();
			fire.Play ();
			networkObject.SendRpc(RPC_SEND_ON_FIRE, Receivers.All);
			return true;
		}
		// If not, just reset the countdown
		m_countDownSpider = burnDuration;
		return false;
	}
}
