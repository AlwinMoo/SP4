using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketLauncher : MonoBehaviour
{
    ObjectPooler objectPooler;
    public float fireRate = 2.0f;
    public GameObject flash;
    private float m_countDown = 0.0f;

    UnityAction Listener;

    // Use this for initialization
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        Listener = new UnityAction(triggerRocket);
    }

    void Update()
    {
        if (m_countDown > 0.0f)
            m_countDown -= Time.deltaTime;
        else if (m_countDown < 0.0f)
            m_countDown = 0.0f;
        /// Rotates the barrel according to the mouse position
        EventManager.StartListening("RocketShoot", Listener, transform.parent.gameObject.tag);
    }

    /// <summary>
    /// Stops the barrel from rotating when player fires
    /// Shoots the rocket bullet
    /// </summary>
    void triggerRocket()
    {

        if (m_countDown > 0.0f)
            return;
        m_countDown += fireRate;

        transform.rotation = Quaternion.LookRotation(transform.parent.GetComponent<VehicleBase>().parentDir);
        flash.GetComponent<RL_Flash>().InitLight();
        objectPooler.SpawnFromPool("RL_Bullet", transform.position, transform.rotation);

        EventManager.StopListening("RocketShoot", Listener, transform.parent.gameObject.tag);
    }
}
