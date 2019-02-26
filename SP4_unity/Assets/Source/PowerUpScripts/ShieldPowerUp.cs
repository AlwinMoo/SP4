using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class ShieldPowerUp : ArmourBehavior {


    void Update()
    {
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up, 45 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<VehicleBase>().armour = 0.5f;


            networkObject.SendRpc(RPC_SEND_DESTROY, Receivers.All);
        }
    }


    public override void SendDestroy(RpcArgs args)
    {
        networkObject.Destroy();
    }
}
