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

        //Gets the sprite from the assigned weapon prefab and applies it to the sprite renderer of this object
        m_weaponSprite.sprite = m_weaponPrefab.GetComponent<SpriteRenderer>( ).sprite;

    }

    public override void SetUpPricePrompt( )
    {
        base.SetUpPricePrompt( );

        //Adds the name of the weapon to the price prompt so the player knows what they're buying
        m_itemPricePrompt.m_productName.text = m_weaponPrefab.name;

    }

    public override void Activated( PlayerController playerController ) 
    {
        //If the player cann afford the weapon, it is bought
        if ( BuyItem( m_playerController.m_currencyManager ) )
        {

            bool weaponAcquired = false;

            //Loops through the player's array of carried weapons, if there is an empty space the weapon is added to it
            for ( int i = 0; i < m_playerController.m_carriedWeapons.Length; i++ )
            {
                if ( m_playerController.m_carriedWeapons[i] == null )
                {
                    //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
                    m_playerController.m_carriedWeapons[i] = Instantiate( m_weaponPrefab , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

                    //Sets the new weapon's transform parent to the player's weapon hold point
                    m_playerController.m_carriedWeapons[i].transform.parent = m_playerController.m_weaponAttachPoint.transform;

                    //Sets the name of the new weapon as the weapon prefab's name, to avoid the new weapon having "(Clone)" in its name
                    m_playerController.m_carriedWeapons[i].name = m_weaponPrefab.name;

                    //Sets the new weapon as inactive so it doesn't conflict with the currently equipped weapon
                    m_playerController.m_carriedWeapons[i].SetActive( false );

                    weaponAcquired = true;

                    break;
                }
            }
            //If the weapon wasn't added to the player's weapon array during the above loop, their currently equipped weapon is replaced by it
            if ( !weaponAcquired )
            {
                //Destroys the player's currently equipped weapon to make room for the new one
                Destroy( m_playerController.m_currentWeapon );

                //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
                m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex] = Instantiate( m_weaponPrefab , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

                //Sets the new weapon's transform parent to the player's weapon hold point
                m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex].transform.parent = m_playerController.m_weaponAttachPoint.transform;

                //Sets the player's current weapon as the newly created weapon, making it look as though they had swapped weapons for it
                m_playerController.m_currentWeapon = m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex];

            }

            //Destroys the store object so it can't be bought more than once
            Destroy( gameObject );
        }
    }

}
