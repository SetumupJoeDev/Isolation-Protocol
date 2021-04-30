using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingShopItem : ShopItem
{

    public int m_healAmount;

    public override void Activated( )
    {
        if ( BuyItem( m_playerController.m_currencyManager ) )
        {

            m_playerController.gameObject.GetComponent<HealthManager>( ).Heal( m_healAmount );

            Destroy( gameObject );
        }

    }

}
