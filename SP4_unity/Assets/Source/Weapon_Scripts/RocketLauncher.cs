﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    ObjectPooler objectPooler;
    public float fireRate = 2.0f;
    public GameObject flash;
    private bool firing = false;
    private float m_countDown = 0.0f;

    // Use this for initialization
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // lower cooldown if cooldown is above 0 and isn't firing
        if (!firing && m_countDown > 0.0f)
            m_countDown -= Time.deltaTime;

        // if right clicked and cooldown is below 0
        if (Input.GetMouseButton(1) && m_countDown <= 0.0f)
        {
            firing = true;
    
            flash.GetComponent<RL_Flash>().InitLight();
            objectPooler.SpawnFromPool("RL_Bullet", transform.position, this.gameObject.transform.rotation);
            m_countDown = fireRate;
            firing = false;

        }
        // If not right clicking and 
    }
    //TODO: add a function to reset the variables if player dies while charging
}
