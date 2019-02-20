using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.Events;

public class FlameThrower : MonoBehaviour {
	// Particle systems
	public ParticleSystem smoke;
	public ParticleSystem fire;
	public ParticleSystem glow;

    UnityAction Listener;

	private bool m_firing;
	// Use this for initialization
	void Start ()
    {
		m_firing = true;
        Listener = new UnityAction(TriggerFire);
    }
		
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            //if (networkObject == null)
            //    return;
            //if (!networkObject.IsServer)
            //{
            //    transform.rotation = networkObject.rotation;
            //    return;
            //}

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane plane = new Plane(Vector3.up, this.transform.position);
            //float distToPlane;

            //if (plane.Raycast(ray, out distToPlane))
            //{
            //    Vector3 hitPos = ray.GetPoint(distToPlane);

            //    Vector3 dir = hitPos - transform.position;
            //    dir.y = 0;
            //    transform.rotation = Quaternion.LookRotation(dir);

            //    //networkObject.rotation = transform.rotation;
            //    //networkObject.SendRpc(RPC_TRIGGER_FIRE, Receivers.All, true);
            //}
            EventManager.StartListening("FireShoot", Listener);
        }
        else
        {
            //if (networkObject != null)
            //    networkObject.SendRpc(RPC_TRIGGER_FIRE, Receivers.All, false);
            EventManager.StopListening("FireShoot", Listener);
            FireEffects(false);
        }
	}

    public void TriggerFire()
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

            //networkObject.rotation = transform.rotation;
            //networkObject.SendRpc(RPC_TRIGGER_FIRE, Receivers.All, true);
            FireEffects(true);
        }

        EventManager.StopListening("FireShoot", Listener);
    }

    public void FireEffects(bool _fire)
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
}
