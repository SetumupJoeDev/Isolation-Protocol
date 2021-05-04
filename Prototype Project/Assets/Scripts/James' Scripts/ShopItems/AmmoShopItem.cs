using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoShopItem : ShopItem
{
    [Range(0, 1)]
    public float m_percentageOfAmmoToRestore;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    [SerializeField]
    private Sprite m_defaultSprite;

    [SerializeField]
    private Sprite m_highlightedSprite;

    public override void Activated( )
    {

        if ( BuyItem( m_playerController.m_currencyManager ) )
        {
            for ( int i = 0; i < m_playerController.m_carriedWeapons.Length; i++ )
            {
                if ( m_playerController.m_carriedWeapons[i] != null )
                {
                    GunBase currentWeapon = m_playerController.m_carriedWeapons[i].GetComponent<GunBase>( );

                    float restoredAmmo = currentWeapon.m_maxAmmoCapacity * m_percentageOfAmmoToRestore;

                    currentWeapon.m_currentCarriedAmmo += ( int )restoredAmmo;

                    if ( currentWeapon.m_currentCarriedAmmo > currentWeapon.m_maxAmmoCapacity )
                    {
                        currentWeapon.m_currentCarriedAmmo = currentWeapon.m_maxAmmoCapacity;
                    }
                }
            }
            Destroy( gameObject );
        }
    }

    public override void ToggleHighlighting( bool highlightActive )
    {

        if ( !highlightActive )
        {
            m_spriteRenderer.sprite = m_defaultSprite;
        }
        else
        {
            m_spriteRenderer.sprite = m_highlightedSprite;
        }

        base.ToggleHighlighting( highlightActive );
    }

    public override void SetUpPricePrompt( )
    {
        base.SetUpPricePrompt( );

        m_itemPricePrompt.m_productName.text = "Ammo Refill";

    }

}
