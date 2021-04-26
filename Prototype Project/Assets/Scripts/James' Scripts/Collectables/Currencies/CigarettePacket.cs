using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarettePacket : CurrencyBase
{

    public override void GetCollected( PlayerController player )
    {
        player.m_currencyManager.m_cigarettePacksCount++;
        base.GetCollected( player );
    }

}
