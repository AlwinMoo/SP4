using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExplosionPS : MonoBehaviour, IPooledObject {

    public ParticleSystem smoke;
    public ParticleSystem explosion;

    public void OnObjectSpawn()
    {
        smoke.Play();
        explosion.Play();
    }
}
