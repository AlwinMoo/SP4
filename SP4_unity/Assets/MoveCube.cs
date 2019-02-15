using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveCube : MoveCubeBehavior{

	private void Update()
	{
		if (!networkObject.IsServer) {
			transform.position = networkObject.position;
			transform.rotation = networkObject.rotation;
			return;
		}
		transform.position += new Vector3 (
			Input.GetAxis ("Horizontal"),
			Input.GetAxis ("Vertical"),
			0
		) * Time.deltaTime * 5.0f;
		transform.Rotate (transform.up, Time.deltaTime * 90.0f);
		// Assign the transform information here
		networkObject.position = transform.position;
		networkObject.rotation = transform.rotation;
	}
}
