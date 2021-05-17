using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarettePacket : CurrencyBase
{

    public override void GetCollected( PlayerController player )
    {
        //Increases the player's cigarette count by one and destroys the gameObject
        player.m_currencyManager.m_cigarettePacksCount++;
        base.GetCollected( player );
    }

}
