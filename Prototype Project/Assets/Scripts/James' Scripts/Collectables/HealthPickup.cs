using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : CollectableBase
{

    public int m_healAmount;

    public override void GetCollected( PlayerController player )
    {
        //Heals the player by a set amount before being destroyed
        player.GetComponent<HealthManager>( ).Heal( m_healAmount );
        base.GetCollected( player );
    }

}
