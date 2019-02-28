using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;
using BeardedManStudios.Forge.Networking;

public class NormalEnemy : EnemyBase, ILiveEntity {

    Rigidbody thisGO;
    public ParticleSystem fire;
    public ParticleSystem glow;
    //TODO: PUT THIS VARIABLE INTO BASE INSTEAD
    public const float burnDuration = 3.0f;
    //TODO: PUT THIS VARIABLE INTO BASE INSTEAD
    public float maxHealth;
    //private bool m_burning;
    private float m_countDownNormal;
    // Use this for initialization
	public Animator anim;
	// Hashes for calling animation triggers
	private int m_aAttackHash = Animator.StringToHash("attack");
    public override void Start ()
    {
        health = 20;
        maxHealth = health;

        thisGO = this.gameObject.GetComponent<Rigidbody>();
        thisGO.mass = 20;
        mass = thisGO.mass;

        enemyType = enemytype.ENEMY_NORMAL;
        agent.speed = 35f;
        m_countDownNormal = 0.0f;
        m_burning = false;
        fire.Stop();
        glow.Stop();
	}

    public override void Update()
    {
        base.Update();

        //if (health <= 0)
       //     Destroy(this.gameObject);
		// Let the server handle fire damage
		CheckAlive();
		if (networkObject.IsServer && m_burning)
        {
            m_countDownNormal -= Time.deltaTime;
            float damage;
            if (m_countDownNormal <= 0.0f)
            {

                m_burning = false;
                // Set damage to be more accurate
                damage = GlobalDamage.g_fireDamageTickRatio * (Time.deltaTime + m_countDownNormal) * maxHealth;
                fire.Stop();
                glow.Stop();
            }
            else
                damage = GlobalDamage.g_fireDamageTickRatio * Time.deltaTime * maxHealth;
            // Apply tick damage based on time delta
            TakeDamage(damage, GlobalDamage.DamageTypes.DAMAGE_FIRE_NORMAL);
        }
    }

	public override void OnTriggerStay(Collider collision)
	{
		base.OnTriggerStay(collision);
		if (anim.GetCurrentAnimatorStateInfo (0).fullPathHash != m_aAttackHash)
			anim.SetTrigger (m_aAttackHash);
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
            //thisGO.velocity = u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N;
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
				health -= _damage * 2f;
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
				TakeTickDamage(_damage * 2f);
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
            m_countDownNormal = burnDuration;
            glow.Play();
            fire.Play();
            networkObject.SendRpc(RPC_SEND_ON_FIRE, Receivers.All);
            return true;
        }
        // If not, just reset the countdown
        m_countDownNormal = burnDuration;
        return false;
    }

	public override void CheckAlive()
	{
		if (health <= 0) 
		{
            // Call an animation blood splatter to play at this location
            ObjectPooler.Instance.SpawnFromPool("BloodSplatter", transform.position, gameObject.transform.rotation);
            networkObject.Destroy();
		}
	}
}
