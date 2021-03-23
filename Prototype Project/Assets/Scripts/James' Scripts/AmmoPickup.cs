using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : CollectableBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetCollected( PlayerDemo player )
    {
        player.ReplenishAmmo( );
    }

}
