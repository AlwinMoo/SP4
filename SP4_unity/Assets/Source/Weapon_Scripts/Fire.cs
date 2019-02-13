using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	void OnParticleCollision(GameObject other)
	{
		Flammable objectOnFire = other.GetComponent<Flammable> ();
		if (objectOnFire != null) 
		{
			// Function returns a bool if the enemy was ignited
			objectOnFire.Ignited ();
		}
	}
}
