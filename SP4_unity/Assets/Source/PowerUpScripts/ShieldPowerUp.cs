using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class ShieldPowerUp : ArmourBehavior {
   

    void OnTriggerEnter(Collider other)
    {

        for (int i = 0; i < 4; ++i)
        {
            if (other.CompareTag("Player" + i))
            {
                PickUp(other);
            }
        }
    }

    void PickUp(Collider Player)
    {
        Player.gameObject.GetComponent<VehicleBase>().armour = 0.5f;
        networkObject.SendRpc(RPC_SEND_DESTROY, Receivers.All);
    }

    public override void SendDestroy(RpcArgs args)
    {
        networkObject.Destroy();
    }
}
