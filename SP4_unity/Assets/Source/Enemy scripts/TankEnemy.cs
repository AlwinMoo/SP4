﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;
using BeardedManStudios.Forge.Networking;

public class TankEnemy : EnemyBase, ILiveEntity {
	public ParticleSystem fire;
	public ParticleSystem glow;
    //TODO: PUT THIS VARIABLE INTO BASE INSTEAD
    public const float burnDuration = 3.0f;
	//TODO: PUT THIS VARIABLE INTO BASE INSTEAD
	public float maxHealth;
	public Animator anim;
	//private bool m_burning;
	private float m_countDownTank;
	// Hashes for calling animation triggers
	private int m_aStaggeredHash;
	private int m_aDeathHash;
	private int m_aAttackHash;
	private bool m_deathPlayed = false;	// For animations to play before destroy()
	private float m_deathTimer = 0.6f;
    Rigidbody thisGO;

    // Use this for initialization
    public override void Start ()
    {
        health = 60;
        maxHealth = health;

        thisGO = this.gameObject.GetComponent<Rigidbody>();
        thisGO.mass = 100;
        mass = thisGO.mass;

        enemyType = enemytype.ENEMY_TANK;
        agent.speed = 15f;
		m_burning = false;
		m_countDownTank = 0.0f;
		fire.Stop ();
		glow.Stop ();

        m_aStaggeredHash = Animator.StringToHash("staggered");
		m_aDeathHash = Animator.StringToHash("death");
		m_aAttackHash = Animator.StringToHash("attack");
	}

    public override void Update()
    {
		CheckAlive ();
		if (m_deathPlayed) {
			m_deathTimer -= Time.deltaTime;
			return;
		}
        base.Update();
        
		if (networkObject.IsServer && m_burning)
		{

            m_countDownTank -= Time.deltaTime;
			float damage;
			if (m_countDownTank <= 0.0f) 
			{
				m_burning = false;

                // Set damage to be more accurate
                damage = GlobalDamage.g_fireDamageTickRatio * (Time.deltaTime + m_countDownTank) * maxHealth;
				fire.Stop ();
				glow.Stop ();
			}
			else
				damage = GlobalDamage.g_fireDamageTickRatio * Time.deltaTime * maxHealth;
			// Apply tick damage based on time delta
			TakeDamage(damage, GlobalDamage.DamageTypes.DAMAGE_FIRE_NORMAL);
		}
    }
    /// <summary>
    /// Starts attacking the vehicle when in the triggerbox
    /// </summary>
    /// <param name="collision"> PlayerVehicle </param>
	public override void OnTriggerStay(Collider collision)
	{
		base.OnTriggerStay(collision);
		if (anim.GetCurrentAnimatorStateInfo (0).fullPathHash != m_aAttackHash)
			anim.SetTrigger (m_aAttackHash);
	}

    /// <summary>
    /// Do momentum transfer when collided
    /// </summary>
    /// <param name="collision"> PlayerVehicle </param>
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
           u1 = thisGO.velocity;
           u2 = collision.gameObject.GetComponent<Rigidbody>().velocity;

           Vector3 N = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;
            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(u1 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u2 - u1), N) * N, ForceMode.VelocityChange);
            this.gameObject.GetComponent<Rigidbody>().AddForce(u2 + ((2 * m2) / (m1 + m2)) * Vector3.Dot((u1 - u2), N) * N, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Deduct health accordingly to what damaged it
    /// </summary>
    /// <param name="_damage"> Damage Value </param>
    /// <param name="_type"> Damage Type </param>
    /// <returns></returns>
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
			case GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE:
				health -= _damage * 2f;
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
			case GlobalDamage.DamageTypes.DAMAGE_AOE_ROCKET_DAMAGE:
				TakeTickDamage(_damage * 2f);
				break;
			}
		}
		return true;
	}

    /// <summary>
    /// Play fire particles on the enemy when they are set on fire.
    /// Tells everyone that THAT specific enemy is on fire and play the particles accordingly
    /// </summary>
    /// <returns> ErrorCheck Boolean </returns>
	public override bool Ignited()
	{
		// If not already burning
		if (!m_burning) 
		{
			// Reset the countdown and play the fire animation
			m_burning = true;
			m_countDownTank = burnDuration;
			glow.Play();
			fire.Play ();
            networkObject.SendRpc(RPC_SEND_ON_FIRE, Receivers.All);
            return true;
		}
		// If not, just reset the countdown
		m_countDownTank = burnDuration;
		return false;
	}

    /// <summary>
    /// Checks if the Enemy is alive. 
    /// If not, spawn blood particles and delete it
    /// </summary>
	public override void CheckAlive ()
	{
		if (health <= 0) 
		{
			if (!m_deathPlayed) {
				m_deathPlayed = true;
                
                ObjectPooler.Instance.SpawnFromPool("BloodSplatter", transform.position, gameObject.transform.rotation);
                anim.SetTrigger (m_aDeathHash);
				return;
			}	
			if (m_deathTimer <= 0.0f)
            {
                networkObject.Destroy();
            }
                
		}
	}
}
