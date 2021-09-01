using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssignmentButton : MonoBehaviour
{

    [Tooltip("The weapon wall linked to this button.")]
    public WeaponWall m_weaponWall;

    public void OnClick( int slotIndex )
    {
        //Assigns the chosen weapon to the chosen slot
        m_weaponWall.AssignWeaponToSlot( slotIndex );
    }

}
