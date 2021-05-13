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
        if( m_playerCurrency.m_fabricatorFuelCount >= m_selectedItemPrice )
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    

    public virtual void BuySelectedItem( )
    {
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
