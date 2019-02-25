using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class HealthPotion : HealthPotionBehavior {
    

    void Update()
    {
        networkObject.rotation.Set(0, networkObject.rotation.y + 10, 0);
    }

	void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < 4; ++i)
        {
            if(other.CompareTag("Player" + i))
            {
                PickUp(other);
            }
        }
    }

    void PickUp(Collider Player)
    {
        // Instantiate the pick up effects later

        Player.GetComponent<VehicleBase>().health += 30;


        // this only does for one of the players need to sync it online
        RemoveGameObject();
    }

    private void RemoveGameObject()
    {
        networkObject.SendRpc(RPC_SEND_DESTROY, Receivers.All);
    }


    public override void SendDestroy(RpcArgs args)
    {
        networkObject.Destroy();
    }
}
