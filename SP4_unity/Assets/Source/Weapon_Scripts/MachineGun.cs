using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class MachineGun : MachineGunBehavior {

	// Reference to object pooler. (Because this will be called at a high rate
	ObjectPooler objectPooler;
	public float fireRate = 0.2f;
	public GameObject flash;
    public AudioClip GunShot;
    AudioSource GunShotSource;


    private float m_countDown = 0.0f;

	private void Start()
	{
        GunShotSource = GameObject.FindGameObjectWithTag("BaseSFX").GetComponent<AudioSource>();
		objectPooler = ObjectPooler.Instance;
        GunShotSource.clip = GunShot;
    }

    void Update()
    {
		m_countDown -= Time.deltaTime;

		if (Input.GetMouseButton (0) && m_countDown <= 0.0f)
        {
            if (!networkObject.IsServer)
            {
                transform.rotation = networkObject.rotation;
                return;
            }

            m_countDown += fireRate;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, this.transform.position);
            float distToPlane;

            if (plane.Raycast(ray, out distToPlane))
            {
                Vector3 hitPos = ray.GetPoint(distToPlane);

                Vector3 dir = hitPos - transform.position;
				dir.y = 0;
                networkObject.SendRpc(RPC_TRIGGER_SHOOT, Receivers.All, dir);
            }
		}
		// More accurate firerate if more than 1 shot is fired in a row
		else if (m_countDown <= 0.0f) 
		{
			m_countDown = fireRate;
		}
	}

    void triggerShoot(Vector3 direction)
    {
        objectPooler.SpawnFromPool("HMG_Bullet", transform.position, Quaternion.LookRotation(direction));

        // Play thegunfire light
        flash.GetComponent<HMG_Flash>().StartLight();
        GunShotSource.volume = SFX.SFXvolchanger.audioSrc.volume;
        GunShotSource.Play();
    }

    public override void triggerShoot(RpcArgs args)
    {
        triggerShoot(args.GetNext<Vector3>());
    }

}