using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExplosionPS : MonoBehaviour, IPooledObject {

    public ParticleSystem smoke;
    public ParticleSystem explosion;

    /// <summary>
    /// Plays Explosion and Smoke Particles
    /// </summary>
    public void OnObjectSpawn()
    {
        smoke.Play();
        explosion.Play();
    }
}
