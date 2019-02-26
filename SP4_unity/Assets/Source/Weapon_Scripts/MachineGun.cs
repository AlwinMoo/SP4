using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine.Events;

public class MachineGun : MonoBehaviour {

	// Reference to object pooler. (Because this will be called at a high rate
	ObjectPooler objectPooler;
	public float fireRate = 0.2f;
	public GameObject flash;
    public AudioClip GunShot;
    AudioSource GunShotSource;

    UnityAction Listener;

    public Vector3 networkDir;
    bool networked;

    private float m_countDown = 0.0f;

    private void Start()
	{
        GunShotSource = GameObject.FindGameObjectWithTag("BaseSFX").GetComponent<AudioSource>();
		objectPooler = ObjectPooler.Instance;
        GunShotSource.clip = GunShot;

        Listener = new UnityAction(triggerShot);

        networked = false;
    }

    void Update()
    {
		if (m_countDown > 0.0f)
			m_countDown -= Time.deltaTime;
		else if (m_countDown < 0.0f)
			m_countDown = 0.0f;
		EventManager.StartListening("MGShoot", Listener, transform.parent.gameObject.tag);
	}

    void triggerShot()
    {
        
		if (m_countDown > 0.0f)
			return;
		m_countDown += fireRate;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(VehicleBase.parentDir), Time.deltaTime * 15);
        objectPooler.SpawnFromPool("HMG_Bullet", transform.position, transform.rotation);

        // Play thegunfire light
        flash.GetComponent<HMG_Flash>().StartLight();
        GunShotSource.volume = SFX.SFXvolchanger.audioSrc.volume;
        GunShotSource.Play();

        EventManager.StopListening("MGShoot", Listener, transform.parent.gameObject.tag);
    }
}