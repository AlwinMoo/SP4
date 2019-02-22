using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePowerUp : MonoBehaviour
{
    // GameObject - Nuke
    public GameObject nuke;
    // The explosion force (how far the explosion will push other gameobjects away)
    public float power = 100.0f;
    // explosion radius
    public float radius = 5.0f;
    // upward force (sends surrounding gameobjects slightly up into the air)
    public float upForce = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nuke == enabled)
        {
            Invoke("NukeExplosion", 5.0f);
        }
    }

    public void NukeExplosion()
    {

        Vector3 explosionPos = nuke.transform.position; // Set explosion position to the gameobject's position
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius); // Set colliders so that gameobjects in the colliders will get pushed away by the explosion
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(power, explosionPos, radius, upForce, ForceMode.Impulse);
            }
        }
        nuke.SetActive(false);
    }
}