using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFabricator : FabricatorStoreBase
{

    public override void BuySelectedItem( )
    {
        if ( PlayerCanAfford( ) && m_selectedItemIndex < m_productList.m_buttons.Count )
        {
            switch ( m_selectedItemIndex )
            {
                case 0:
                    {
                        if ( !m_productList.GetStoreProduct(m_selectedItemIndex).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Sniper Rifle!" );
                        }
                    }
                    break;

                case 1:
                    {
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Pistol!" );
                        }
                    }
                    break;

                case 2:
                    {
                        if ( !m_productList.GetStoreProduct( m_selectedItemIndex ).GetIsUnlocked( ) )
                        {
                            m_productList.GetStoreProduct( m_selectedItemIndex ).SetIsUnlocked( true );
                            Debug.Log( "Unlocked the Burst Rifle!" );
                        }
                    }
                    break;

                case 3:
                    {
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
