using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : CollectableBase
{

    [Tooltip("The amount of health to restore to the player on collection.")]
    public int m_healAmount;

    public override void GetCollected( PlayerController player )
    {
        //Heals the player by a set amount before being destroyed
        player.GetComponent<HealthManager>( ).Heal( m_healAmount );

        base.GetCollected( player );
    }

}
