using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : CollectableBase
{

    public override void GetCollected( PlayerController player )
    {
        //Replenishes the player's ammo before being destroyed
        player.ReplenishAmmo( );
        base.GetCollected( player );
    }

}
