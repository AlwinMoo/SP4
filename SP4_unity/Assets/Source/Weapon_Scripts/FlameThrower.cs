using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {
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
		if (_fire) {
			smoke.Play ();
			fire.Play ();
			glow.Play ();
		}
		else 
		{
			smoke.Stop ();
			fire.Stop ();
			glow.Stop ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0))
			TriggerFire (true);
		else if (Input.GetMouseButtonUp(0))
			TriggerFire (false);
	}
}
