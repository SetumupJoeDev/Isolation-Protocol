using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUpgradeFabricator : FabricatorStoreBase
{
    [SerializeField]
    private GameObject m_combatDrone;

    //This code is inefficient and inflexible, aiming to replace this in production

    public override void BuySelectedItem( )
    {
        //Checks to see if the player can afford the item, and that the item index is within the bounds of the list, before switching through the products
        if ( PlayerCanAfford( ) && m_selectedItemIndex < m_productList.m_buttons.Count )
        {
            switch ( m_selectedItemIndex )
            {
                //If the item index is 0, the product is the Drone
                case 0:
                    {
                        //If the drone is not already unlocked, it is set as active and unlocked
                        if( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_combatDrone.SetActive( true );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise a message is sent in the console (Will be removed later once buttons can be set to display "Sold Out"
                        else
                        {
                            Debug.Log( "Item already unlocked!" );
                        }
                    }
                    break;
                //If the item index is 1, the product is the Drone's search mode
                case 1:
                    {
                        //If the search mode is not already unlocked, it is set as unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked search mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise, any active drone upgrades are disabled and the search mode is enabled
                        else
                        {
                            Debug.Log( "Activated search mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<SearchMode>( ).enabled = true;
                        }
                    }
                    break;
                //If the item index is 2, the product is the drone's shield mode
                case 2:
                    {
                        //If the shield mode is not already unlocked, it is set as unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Shield Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise, any active drone upgrades are disabled and the shield mode is enabled
                        else
                        {
                            Debug.Log( "Activated Shield Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<ShieldMode>( ).enabled = true;
                        }
                    }
                    break;
                //If the item index is 3, then the product is the drone's shockwave mode
                case 3:
                    {
                        //If the shockwave mode is not already unlocked, it is set as unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Shockwave Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise, any active drone upgrades are disabled and the shockwave mode is enabled
                        else
                        {
                            Debug.Log( "Activated Shockwave Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<ShockwaveMode>( ).enabled = true;
                        }
                    }
                    break;
                //If the item index if 4, then the product is the drone's sentry mode upgrade
                case 4:
                    {
                        //If the turret mode is not already unlocked, it is set as unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Turret Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise, any active drone upgrades are disabled and the turret mode is enabled
                        else
                        {
                            Debug.Log( "Activated Turret Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<SentryMode>( ).enabled = true;
                        }
                    }
                    break;
                //If the item index is 5, the product is the drone's defib upgrade
                case 5:
                    {
                        //If the defib mode is not already unlocked, it is set as unlocked
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Debrillator Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        //Otherwise, any active drone upgrades are disabled and the defib mode is enabled
                        else
                        {
                            Debug.Log( "Activated Defibrillator Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<DefibMode>( ).enabled = true;
                        }
                    }
                    break;
            }

            m_playerCurrency.m_fabricatorFuelCount -= m_selectedItemPrice;

        }
    }
}
