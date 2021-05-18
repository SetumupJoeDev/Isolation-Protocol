using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorStoreBase : MonoBehaviour
{

    [SerializeField]
    protected ButtonListGenerator m_productList;

    [SerializeField]
    protected CurrencyManager m_playerCurrency;

    protected int m_selectedItemIndex;

    protected int m_selectedItemPrice;

    public bool PlayerCanAfford( )
    {
        //If the player's fuel count is greater than or equal to the price of the selected product, they can afford it and this method returns true
        if( m_playerCurrency.m_fabricatorFuelCount >= m_selectedItemPrice )
        {
            return true;
        }
        //Otherwise, they can't afford it and this method returns false
        else
        {
            return false;
        }
    } 
    

    public virtual void BuySelectedItem( )
    {
        //If the player can afford the product, and the item is within the bounds of the list, the product is bought
        if ( PlayerCanAfford( ) && m_selectedItemIndex < m_productList.m_buttons.Count )
        {
            Debug.Log( "Item bought!" );
        }
    }

    public void SetItemIndex( int newIndex )
    {
        m_selectedItemIndex = newIndex;
    }

    public void SetItemPrice( int newPrice )
    {
        m_selectedItemPrice = newPrice;
    }

}
