using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUpgradeFabricator : FabricatorStoreBase
{
    [SerializeField]
    private GameObject m_combatDrone;

    public override void BuySelectedItem( )
    {

        if ( PlayerCanAfford( ) && m_selectedItemIndex < m_productList.m_buttons.Count )
        {
            switch ( m_selectedItemIndex )
            {
                case 0:
                    {
                        if( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_combatDrone.SetActive( true );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        else
                        {
                            Debug.Log( "Item already unlocked!" );
                        }
                    }
                    break;

                case 1:
                    {
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked search mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        else
                        {
                            Debug.Log( "Activated search mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<SearchMode>( ).enabled = true;
                        }
                    }
                    break;

                case 2:
                    {
                        if( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Shield Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        else
                        {
                            Debug.Log( "Activated Shield Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<ShieldMode>( ).enabled = true;
                        }
                    }
                    break;

                case 3:
                    {
                        if( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Shockwave Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        else
                        {
                            Debug.Log( "Activated Shockwave Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<ShockwaveMode>( ).enabled = true;
                        }
                    }
                    break;

                case 4:
                    {
                        if(!m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Turret Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
                        else
                        {
                            Debug.Log( "Activated Turret Mode!" );
                            m_combatDrone.GetComponent<DroneController>( ).DisableAllUpgrades( );
                            m_combatDrone.GetComponent<SentryMode>( ).enabled = true;
                        }
                    }
                    break;

                case 5:
                    {
                        if(!m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            Debug.Log( "Unlocked Debrillator Mode!" );
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                        }
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
