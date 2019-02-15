using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
// For RPC
using BeardedManStudios.Forge.Networking;
using System;
public class CameraFollow : PlayerCameraBehavior {
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.


    Vector3 offset;                     // The initial offset from the target.


    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
		// If this doesn't belong to the server, update info only
		if (!networkObject.IsServer) {
			transform.position = networkObject.position;
			return;
		}
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

		networkObject.position = transform.position;
    }
	// TODO: Decide if this function is needed (only if camera can rotate) REMOVE FOR NOW
	public override void InitRotation(RpcArgs args)
	{
		transform.rotation = args.GetNext<Quaternion> ();
	}
}
