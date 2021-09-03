using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWall : UpgradeManager
{

    [Tooltip("The selected weapon, chosen by clicking one of the weapon selection buttons.")]
    public GameObject m_selectedWeapon;

    [Tooltip("An array containing all of the prefabs of the weapons that can be equipped.")]
    public GameObject[] m_weaponPrefabs;

    public virtual void AssignWeaponToSlot( int chosenSlot )
    {
        //Creates a temporary array to store the player's current weapons
        GameObject[] tempArray = m_playerController.m_carriedWeapons;

        //Resizes the player's weapons array according to its current length and the index of the chosen weapon slot
        if ( chosenSlot == 2 && m_playerController.m_carriedWeapons.Length <= 3 || m_playerController.m_carriedWeapons.Length == 3)
        {
            m_playerController.m_carriedWeapons = new GameObject[3];
        }
        else
        {
            m_playerController.m_carriedWeapons = new GameObject[2];
        }

        //If the temporary array is shoter or the same length as the player's new weapons array, then we loop through and assign all of the weapons in the temp array to the player's new weapons array
        if ( tempArray.Length <= m_playerController.m_carriedWeapons.Length )
        {
            for ( int i = 0; i < tempArray.Length; i++ )
            {
                m_playerController.m_carriedWeapons[i] = tempArray[i];

                //Sets the new weapon's transform parent to the player's weapon hold point
                m_playerController.m_carriedWeapons[i].transform.parent = m_playerController.m_weaponAttachPoint.transform;
            }
        }
        //Otherwise, we instantiate just the first in the array of the temp array to that of the new weapons array
        else
        {
            m_playerController.m_carriedWeapons[0] = tempArray[0];

            //Sets the new weapon's transform parent to the player's weapon hold point
            m_playerController.m_carriedWeapons[0].transform.parent = m_playerController.m_weaponAttachPoint.transform;
        }

        //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
        m_playerController.m_carriedWeapons[chosenSlot] = Instantiate( m_selectedWeapon , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

        //Sets the new weapon's transform parent to the player's weapon hold point
        m_playerController.m_carriedWeapons[chosenSlot].transform.parent = m_playerController.m_weaponAttachPoint.transform;

        //Sets the player's current weapon as the newly created weapon, making it look as though they had swapped weapons for it
        m_playerController.m_currentWeapon = m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex];

        //Disables the weapon so it can't be used in the hub area
        m_playerController.m_carriedWeapons[chosenSlot].SetActive( false );

        SaveSystem.SavePlayer(m_playerController);
    }

    public void ChangeSelectedWeapon( int weaponIndex )
    {
        //Sets the selected weapon as that at the array index passed in by the button
        m_selectedWeapon = m_weaponPrefabs[weaponIndex];
    }

}
