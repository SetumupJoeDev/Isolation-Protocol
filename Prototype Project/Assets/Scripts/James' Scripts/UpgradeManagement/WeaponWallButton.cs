using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWallButton : InterfaceButton
{

    [Tooltip("The weapon wall associated with the button.")]
    public WeaponWall m_weaponWall;

    public override void OnClick( )
    {
        //Changes the weapon wall's selected weapon by passing in a new array index
        m_weaponWall.ChangeSelectedWeapon( m_buttonID );
    }

}
