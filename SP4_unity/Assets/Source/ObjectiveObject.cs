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

    // Use this for initialization
    void Awake()
    {
        health = 50;
        HealthSlider.maxValue = health;
        HealthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = health;
    }

    public void SendRemove()
    {
        networkObject.SendRpc(RPC_SEND_REMOVE, Receivers.All);
    }


    public override void SendRemove(RpcArgs args)
    {
        networkObject.Destroy();
    }

}