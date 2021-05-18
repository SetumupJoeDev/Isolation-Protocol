using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingShopItem : ShopItem
{

    public int m_healAmount;

    public override void Activated( )
    {
        //If the player can afford the item, it is bought and their health is restored
        if ( BuyItem( m_playerController.m_currencyManager ) )
        {
            //The player is healed by the value of healAmount
            m_playerController.gameObject.GetComponent<HealthManager>( ).Heal( m_healAmount );

            //The object is destroyed so it cannot be bought and used again
            Destroy( gameObject );
        }

    }

    public override void SetUpPricePrompt( )
    {
        base.SetUpPricePrompt( );

        //The item's price prompt is set up to display the product's name
        m_itemPricePrompt.m_productName.text = "Heal Up";

    }

}
