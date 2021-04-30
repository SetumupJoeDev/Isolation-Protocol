using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopItem : ShopItem
{

    public GameObject m_weaponPrefab;

    public SpriteRenderer m_weaponSprite;

    public override void Start( )
    {
        base.Start( );

        m_weaponSprite.sprite = m_weaponPrefab.GetComponent<SpriteRenderer>( ).sprite;

    }

    public override void Activated( ) 
    {

        if ( BuyItem( m_playerController.m_currencyManager ) )
        {

            bool weaponAcquired = false;

            for ( int i = 0; i < m_playerController.m_carriedWeapons.Length; i++ )
            {
                if ( m_playerController.m_carriedWeapons[i] == null )
                {
                    m_playerController.m_carriedWeapons[i] = Instantiate( m_weaponPrefab , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

                    m_playerController.m_carriedWeapons[i].transform.parent = m_playerController.m_weaponAttachPoint.transform;

                    m_playerController.m_carriedWeapons[i].SetActive( false );

                    weaponAcquired = true;

                    break;
                }
            }
            if ( !weaponAcquired )
            {
                m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex] = m_weaponPrefab;
            }
        }
    }

}
