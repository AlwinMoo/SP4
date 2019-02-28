using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class ObjectiveObject : ObjectiveObjectBehavior
{

    public int health;
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
        this.gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(4, 4, 4), Time.deltaTime * 1);

    
         HealthSlider.value = health;

         if (remove)
         {
             temp -= Time.deltaTime;
             this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, Vector3.zero, Time.deltaTime * 1);
             if (temp <= 0 && networkObject.IsServer)
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