using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableWeapon : InteractableObject
{

    [Tooltip("The prefab of the weapon this object represents.")]
    public GameObject m_weaponPrefab;

    public override void Activated(PlayerController playerController)
    {
        
        //Runs the base activation code
        base.Activated(playerController);

        //Equips the weapon to the player
        EquipToPlayer( );

    }

    public void EquipToPlayer( )
    {

        if( m_playerController.m_carriedWeapons.Length < 4 )
        {
            //Creates a temporary array containing the player's current weapons
            GameObject[] tempArray = m_playerController.m_carriedWeapons;

            //Resizes the player's weapons array to hold the new weapon
            m_playerController.m_carriedWeapons = new GameObject[tempArray.Length + 1];

            //Loops through the temporary array and adds the weapons from it to the player's new weapon array
            for ( int i = 0; i < tempArray.Length; i++ )
            {
                m_playerController.m_carriedWeapons[i] = Instantiate( tempArray[i] , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );
            }

            //Creates an int variable used to place the weapon at the end of the weapons array
            int newWeaponIndex = m_playerController.m_carriedWeapons.Length - 1;

            //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
            m_playerController.m_carriedWeapons[ newWeaponIndex ] = Instantiate( m_weaponPrefab , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

            //Sets the new weapon's transform parent to the player's weapon hold point
            m_playerController.m_carriedWeapons[newWeaponIndex].transform.parent = m_playerController.m_weaponAttachPoint.transform;

            //Sets the name of the new weapon as the weapon prefab's name, to avoid the new weapon having "(Clone)" in its name
            m_playerController.m_carriedWeapons[newWeaponIndex].name = m_weaponPrefab.name;

            //Sets the new weapon as inactive so it doesn't conflict with the currently equipped weapon
            m_playerController.m_carriedWeapons[newWeaponIndex].SetActive( false );

            

        }

        //If the weapon wasn't added to the player's weapon array during the above loop, their currently equipped weapon is replaced by it
        else
        {
            //Destroys the player's currently equipped weapon to make room for the new one
            Destroy(m_playerController.m_currentWeapon);

            //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
            m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex] = Instantiate(m_weaponPrefab, m_playerController.m_weaponAttachPoint.transform.position, Quaternion.identity);

            //Sets the new weapon's transform parent to the player's weapon hold point
            m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex].transform.parent = m_playerController.m_weaponAttachPoint.transform;

            //Sets the player's current weapon as the newly created weapon, making it look as though they had swapped weapons for it
            m_playerController.m_currentWeapon = m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex];

        }

        //Destroys the store object so it can't be bought more than once
        Destroy(gameObject);
    }

}
