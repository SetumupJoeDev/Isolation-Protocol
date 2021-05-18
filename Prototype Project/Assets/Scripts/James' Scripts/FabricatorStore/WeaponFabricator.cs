using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFabricator : FabricatorStoreBase
{

    //This code is inefficient and inflexible, aiming to replace this in production

    public override void BuySelectedItem( )
    {
        //Checks to see if the player can afford the item, and that the item index is within the bounds of the list, before switching through the products
        if ( PlayerCanAfford( ) && m_selectedItemIndex < m_productList.m_buttons.Count )
        {
            switch ( m_selectedItemIndex )
            {
                //If the item index is 0, the product is the Sniper Rifle
                case 0:
                    {
                        //If the weapon is not already unlocked, it is set as active and unlocked
                        if ( !m_productList.GetStoreProduct(m_selectedItemIndex).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Sniper Rifle!" );
                        }
                    }
                    break;

                //If the item index is 0, the product is the Pistol
                case 1:
                    {
                        //If the weapon is not already unlocked, it is set as active and unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Pistol!" );
                        }
                    }
                    break;

                //If the item index is 0, the product is the Burst Rifle
                case 2:
                    {
                        //If the weapon is not already unlocked, it is set as active and unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Burst Rifle!" );
                        }
                    }
                    break;

                //If the item index is 0, the product is the Submachine Gun
                case 3:
                    {
                        //If the weapon is not already unlocked, it is set as active and unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Submachine Gun!" );
                        }
                    }
                    break;

            }
        }
    }

}
