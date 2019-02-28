using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterPS : MonoBehaviour, IPooledObject {

    public ParticleSystem blood;

    public void OnObjectSpawn()
    {
        blood.Play();
    }
}
