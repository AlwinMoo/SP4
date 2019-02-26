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
    UnityAction CancelListener;

    private bool m_firing;
	// Use this for initialization
	void Start ()
    {
		m_firing = false;
        Listener = new UnityAction(TriggerFire);
        CancelListener = new UnityAction(ResetFire);
    }
		
	// Update is called once per frame
	void Update ()
    {
        EventManager.StartListening("CancelFire", CancelListener, transform.parent.gameObject.tag);
        EventManager.StartListening("FireShoot", Listener, transform.parent.gameObject.tag);
    }

    public void TriggerFire()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(VehicleBase.parentDir), Time.deltaTime * 2);
        FireEffects(true);

        EventManager.StopListening("FireShoot", Listener, transform.parent.gameObject.tag);
    }

    public void ResetFire()
    {
        FireEffects(false);
        m_firing = false;

        EventManager.StartListening("CancelFire", CancelListener, transform.parent.gameObject.tag);
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
            m_firing = false;
        }
    }
}
