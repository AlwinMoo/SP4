using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class HealthPotion : HealthPotionBehavior {
    
    void Start()
    {

    }

    void Update()
    {
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up, 45 * Time.deltaTime);
    }

	void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < 4; ++i)
        {
            if(other.CompareTag("Player" + i))
            {
                Debug.Log("Player" + i);
                PickUp(other);
            }
        }
    }

    void PickUp(Collider Player)
    {
        // Instantiate the pick up effects later

        Player.GetComponent<VehicleBase>().health += 30;

        Debug.Log("Removed");
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
