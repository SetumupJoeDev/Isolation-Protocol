using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : InteractableObject
{

    [Range(1, 2)]
    [Tooltip("A modifier for the item's price based on the difficulty of the level.")]
    public float m_priceModifier;
    
    [Tooltip("The base price of this item when it is bought.")]
    public int m_basePrice;

    [Tooltip("The price of this item.")]
    public int m_itemPrice;

    public PricePromptController m_itemPricePrompt;

    // Start is called before the first frame update
    public virtual void Start()
    {

        //Calculates the price of the product based on its base price and the price modifier
        float modifiedPrice = m_basePrice * m_priceModifier;

        m_itemPrice = (int)modifiedPrice;

        //Sets up the item's price prompt with the newly calculated price
        SetUpPricePrompt( );

    }

    public virtual void SetUpPricePrompt( )
    {
        //Sets the price text of the price prompt to the item's price
        m_itemPricePrompt.m_productPrice.text = m_itemPrice.ToString( );
    }

    public override void Activated( PlayerController playerController )
    {
        //Removes currency from the player's inventory and activates the item
        BuyItem( m_playerController.gameObject.GetComponent<CurrencyManager>( ) );
        base.Activated( playerController );
    }

    public virtual bool BuyItem( CurrencyManager playerCurrency )
    {
        //If the player can afford the item, the price of the item is deducted from their currency count and the item is bought
        if( playerCurrency.m_cigarettePacksCount >= m_itemPrice )
        {
            playerCurrency.m_cigarettePacksCount -= m_itemPrice;
            Debug.Log( "Cha-ching!" );
            return true;
        }
        else
        {
            Debug.Log( "Insufficient funds!" );
            return false;
        }
    }

}
