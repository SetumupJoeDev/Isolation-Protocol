using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoShopItem : ShopItem
{

    [Tooltip("The percentages of the player's weapons' ammo to restore when collected")]
    [Range(0, 1)]
    public float m_percentageOfAmmoToRestore;

    [Tooltip("The sprite renderer attached to this object.")]
    public SpriteRenderer m_spriteRenderer;

    [Tooltip("The ammo item's default sprite.")]
    public Sprite m_defaultSprite;

    [Tooltip("The ammo item's highlighted sprite.")]
    public Sprite m_highlightedSprite;

    public override void Activated( PlayerController playerController )
    {
        //If the player can afford the item, their weapons' ammo is refilled
        if ( BuyItem( m_playerController.m_currencyManager ) )
        {
            //Loops through each of the player's weapons, restoring some ammo of each
            for ( int i = 0; i < m_playerController.m_carriedWeapons.Length; i++ )
            {
                //If the current index in the player's weapons array isn't empty, its ammo is restored
                if ( m_playerController.m_carriedWeapons[i] != null )
                {
                    //Saves the weapon's script to a variable for later use
                    GunBase currentWeapon = m_playerController.m_carriedWeapons[i].GetComponent<GunBase>( );

                    bool isWeaponActive = currentWeapon.gameObject.activeSelf;

                    //The weapon is set as active so that its script can be accessed
                    currentWeapon.gameObject.SetActive( true );

                    //Calculates the amount of ammo to restore based on the maximum capacity of the weapon and the percentage to restore
                    float restoredAmmo = currentWeapon.m_maxAmmoCapacity * m_percentageOfAmmoToRestore;

                    //Increases the weapon's carried ammo by the value calculated above
                    currentWeapon.m_currentCarriedAmmo += ( uint )restoredAmmo;

                    //If the carried ammo now surpases the maximum, it is set to the maximum
                    if ( currentWeapon.m_currentCarriedAmmo > currentWeapon.m_maxAmmoCapacity )
                    {
                        currentWeapon.m_currentCarriedAmmo = currentWeapon.m_maxAmmoCapacity;
                    }


                    //Deactivates the weapon if it wasn't already active before restoring ammo
                    if ( !isWeaponActive )
                    {
                        currentWeapon.gameObject.SetActive( false );
                    }

                }
            }
            //Destroys the ammo item so it cannot be bought and used again
            Destroy( gameObject );
        }
    }

    public override void ToggleHighlighting( bool highlightActive )
    {
        //Toggles the highlighting on or off depending on the value passed in
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

        //Sets up the price prompt with the product's name
        m_itemPricePrompt.m_productName.text = "Ammo Refill";

    }

}
