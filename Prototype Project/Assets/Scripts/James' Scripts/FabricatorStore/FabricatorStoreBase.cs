using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorStoreBase : MonoBehaviour
{

    [Tooltip("The currency manager of the player.")]
    public CurrencyManager m_playerCurrency;

    [Tooltip("The sound to play when the player purchases a product.")]
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
        //Subtracts the price of the purchased prodcut from the player's fuel count
        m_playerCurrency.m_fabricatorFuelCount -= amountToCharge;

        //Plays the item purchased sound
        m_itemPurchased.Play( );

    }

}
