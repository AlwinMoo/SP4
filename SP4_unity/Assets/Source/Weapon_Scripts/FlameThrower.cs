using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class FlameThrower : FlamethrowerBehavior {
	// Particle systems
	public ParticleSystem smoke;
	public ParticleSystem fire;
	public ParticleSystem glow;

	private bool m_firing;
	// Use this for initialization
	void Start () {
		m_firing = true;
	}
		
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            if (networkObject == null)
                return;
            if (!networkObject.IsServer)
            {
                transform.rotation = networkObject.rotation;
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, this.transform.position);
            float distToPlane;

            if (plane.Raycast(ray, out distToPlane))
            {
                Vector3 hitPos = ray.GetPoint(distToPlane);

                Vector3 dir = hitPos - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);

                networkObject.rotation = transform.rotation;
                networkObject.SendRpc(RPC_TRIGGER_FIRE, Receivers.All, true);
            }
        }
        else
        {
            if (networkObject != null)
                networkObject.SendRpc(RPC_TRIGGER_FIRE, Receivers.All, false);
        }
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
        }
        else
        {
            smoke.Stop();
            fire.Stop();
            glow.Stop();
        }
    }

    public override void TriggerFire(RpcArgs args)
    {
        TriggerFire(args.GetNext<bool>());
    }
}
