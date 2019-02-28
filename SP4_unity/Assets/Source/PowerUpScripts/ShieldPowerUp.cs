using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class ShieldPowerUp : ArmourBehavior {
   
    /// <summary>
    ///  Check if a player picked up the powerup
    /// </summary>
    /// <param name="other"> the player </param>
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
    /// <summary>
    ///  if pick up delete the gameobject
    /// </summary>
    /// <param name="Player"> the player that picked up the power up </param>
    void PickUp(Collider Player)
    {
        Player.gameObject.GetComponent<VehicleBase>().armour *= 0.5f;
        networkObject.SendRpc(RPC_SEND_DESTROY, Receivers.All);
    }

    public override void SendDestroy(RpcArgs args)
    {
        networkObject.Destroy();
    }
}
