using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorStoreBase : MonoBehaviour
{

    [SerializeField]
    protected ButtonListGenerator m_productList;

    [SerializeField]
    protected CurrencyManager m_playerCurrency;

    public AudioSource m_itemPurchased;

    public bool PlayerCanAfford( int itemPrice )
    {
        //If the player's fuel count is greater than or equal to the price of the selected product, they can afford it and this method returns true
        if( m_playerCurrency.m_fabricatorFuelCount >= itemPrice )
        {
            return true;
        }
        //Otherwise, they can't afford it and this method returns false
        else
        {
            return false;
        }
    }

    public void ChargePlayer( int amountToCharge )
    {

        m_playerCurrency.m_fabricatorFuelCount -= amountToCharge;

        m_itemPurchased.Play( );

    }

}
