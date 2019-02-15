//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
// Use these
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;

public class Guy : GuyBehavior {
	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			networkObject.SendRpc (RPC_MOVE_UP, Receivers.All);
	}

	public override void MoveUp(RpcArgs args)
	{
		transform.position += Vector3.up;
	}
}
