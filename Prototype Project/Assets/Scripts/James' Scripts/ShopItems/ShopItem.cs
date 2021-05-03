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
        float modifiedPrice = m_basePrice * m_priceModifier;

        m_itemPrice = (int)modifiedPrice;

        SetUpPricePrompt( );

        Debug.Log( m_itemPrice );

    }

    public virtual void SetUpPricePrompt( )
    {
        m_itemPricePrompt.m_productPrice.text = m_itemPrice.ToString( );
    }

    public virtual void DisplayPrice( bool priceDisplayed )
    {

        m_itemPricePrompt.gameObject.SetActive( priceDisplayed );

    }

    public override void Activated( )
    {
        BuyItem( m_playerController.gameObject.GetComponent<CurrencyManager>( ) );
        base.Activated( );
    }

    public override void ToggleHighlighting( bool highlightActive )
    {
        base.ToggleHighlighting( highlightActive );

        DisplayPrice( m_highlightingActive );

    }

    public virtual bool BuyItem( CurrencyManager playerCurrency )
    {
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
