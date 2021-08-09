using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWall : UpgradeManager
{

    public GameObject m_selectedWeapon;

    public GameObject[] m_weaponPrefabs;

    public virtual void AssignWeaponToSlot( int chosenSlot )
    {

        Destroy( m_playerController.m_carriedWeapons[chosenSlot] );

        //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
        m_playerController.m_carriedWeapons[chosenSlot] = Instantiate( m_selectedWeapon , m_playerController.m_weaponAttachPoint.transform.position , Quaternion.identity );

        //Sets the new weapon's transform parent to the player's weapon hold point
        m_playerController.m_carriedWeapons[chosenSlot].transform.parent = m_playerController.m_weaponAttachPoint.transform;

        //Sets the player's current weapon as the newly created weapon, making it look as though they had swapped weapons for it
        m_playerController.m_currentWeapon = m_playerController.m_carriedWeapons[m_playerController.m_currentWeaponIndex];

        //Disables the weapon so it can't be used in the hub area
        m_playerController.m_carriedWeapons[chosenSlot].SetActive( false );

    }

    public void ChangeSelectedWeapon( int weaponIndex )
    {
        m_selectedWeapon = m_weaponPrefabs[weaponIndex];
    }

}
