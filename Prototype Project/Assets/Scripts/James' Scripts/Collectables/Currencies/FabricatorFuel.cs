using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorFuel : CurrencyBase
{

    public override void GetCollected( PlayerController player )
    {
        //Increases the number of fabricator fuel the player has collected and then destroys the object
        player.m_currencyManager.m_fabricatorFuelCount++;
        player.m_currencyManager.m_totalFabricatorFuelCount++;
        base.GetCollected( player );
    }

}
