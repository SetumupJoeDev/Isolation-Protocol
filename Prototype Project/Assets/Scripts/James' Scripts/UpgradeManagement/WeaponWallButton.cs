using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWallButton : InterfaceButton
{

    public WeaponWall m_weaponWall;

    public override void OnClick( )
    {
        m_weaponWall.ChangeSelectedWeapon( m_buttonID );
    }

}
