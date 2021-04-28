using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : CollectableBase
{

    public int m_healAmount;

    public override void GetCollected( PlayerController player )
    {
        player.GetComponent<HealthManager>( ).Heal( m_healAmount );
        base.GetCollected( player );
    }

}
