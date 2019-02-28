using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterPS : MonoBehaviour, IPooledObject {

    public ParticleSystem blood;

    /// <summary>
    /// Plays Blood Particles
    /// </summary>
    public void OnObjectSpawn()
    {
        blood.Play();
    }
}
