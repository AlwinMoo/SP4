using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;
public class FlameThrower : w_ftBehavior {
	// Particle systems
	public ParticleSystem smoke;
	public ParticleSystem fire;
	public ParticleSystem glow;

	private bool m_firing;
	// Use this for initialization
	void Start () {
		m_firing = true;
	}
		
	public void TriggerFire(bool _fire)
	{
		if (m_firing == _fire)
			return;
		m_firing = _fire;
		if (_fire)
        {
           smoke.Play();
           fire.Play();
           glow.Play();
			// Since there's a change in flamethrower state, send the rpc to all
			networkObject.SendRpc (RPC_TOGGLE_FIRE, Receivers.All, true);
		}
		else 
		{
			smoke.Stop ();
			fire.Stop ();
			glow.Stop ();
			// Since there's a change in flamethrower state, send the rpc to all
			networkObject.SendRpc (RPC_TOGGLE_FIRE, Receivers.All, false);
		}
	}
	// Update is called once per frame
	void Update () {
		if (!networkObject.IsServer)
			return;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, this.transform.position);
            float distToPlane;

            if (plane.Raycast(ray, out distToPlane))
            {
                Vector3 hitPos = ray.GetPoint(distToPlane);

                Vector3 dir = hitPos - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);
                TriggerFire(true);
            }
        }
        else
            TriggerFire(false);
	}

	public override void ToggleFire(RpcArgs args)
	{
		TriggerFire (args.GetNext<bool> ());
	}
}
