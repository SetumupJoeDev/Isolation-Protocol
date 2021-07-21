using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssignmentButton : MonoBehaviour
{

    public WeaponWall m_weaponWall;

    public void OnClick( int slotIndex )
    {
        m_weaponWall.AssignWeaponToSlot( slotIndex );
    }

}
