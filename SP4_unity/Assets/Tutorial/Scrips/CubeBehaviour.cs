using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : Bolt.EntityBehaviour<ICubeState> {

	// Like Start, but only called after object exists in network
	public override void Attached()
	{
		state.SetTransforms (state.CubeTransform, transform);
	}
	
	public override void SimulateOwner()
	{
		var speed = 4f;
		var movement = Vector3.zero;

		if (Input.GetKey (KeyCode.W)) { movement.z += 1; }
		if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
		if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
		if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

		// Avoid divide by zero
		if (movement != Vector3.zero)
		{
			transform.position = transform.position + (movement.normalized * speed * BoltNetwork.frameDeltaTime);
		}
	}
}
