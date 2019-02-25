using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class ObjectiveObject : ObjectiveObjectBehavior
{

    private int health;
    public Slider HealthSlider;
    private bool remove;
    private float temp;

    // Use this for initialization
    void Awake()
    {
        health = 50;
        HealthSlider.maxValue = health;
        HealthSlider.value = health;
        temp = 3;
        remove = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.y >= 3)
        {
           // gameObject.transform.position.Set(gameObject.transform.position.x, 3, gameObject.transform.position.z);

            float newTrf = gameObject.transform.position.y;
            newTrf -= 9.81f;
            gameObject.transform.position.Set(gameObject.transform.position.x, newTrf, gameObject.transform.position.z);

            //Debug.Log(gameObject.transform.position);
        }
        else
        {
            gameObject.transform.position.Set(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        }

        HealthSlider.value = health;

        if(remove)
        {
            temp -= Time.deltaTime;
            gameObject.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y + 10, gameObject.transform.position.z);
            Debug.Log(temp);
            if (temp <= 0)
            {
                networkObject.SendRpc(RPC_SEND_REMOVE, Receivers.All);
            }
        }

    }

    public void SendRemove()
    {
        remove = true;
    }


    public override void SendRemove(RpcArgs args)
    {
        networkObject.Destroy();
    }

}