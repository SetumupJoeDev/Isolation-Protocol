using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorFuel : CurrencyBase
{

    public override void GetCollected( PlayerController player )
    {
        player.m_currencyManager.m_fabricatorFuelCount++;
        base.GetCollected( player );
    }

}
