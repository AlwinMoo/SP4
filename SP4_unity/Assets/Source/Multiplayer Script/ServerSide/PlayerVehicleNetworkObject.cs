using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
public partial class PlayerVehicleNetworkObjectO : PlayerVehicleNetworkObject {

    protected override bool AllowOwnershipChange(NetworkingPlayer newOwner)
    {
        // The newOwner is the NetworkingPlayer that is requesting the ownership change, you can get the current owner with just "Owner"
        // First check the newOwner's ID and see if he matches that car
        
        return true;
    }
}
