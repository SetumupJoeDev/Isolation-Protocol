using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : CharacterBase
{

    [Header("Weapons")]
    [Tooltip("The weapon held by the player.")]
    public WeaponBase m_currentWeapon;

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        m_directionalVelocity.y = Input.GetAxisRaw("Vertical");

        if( Input.GetMouseButtonDown( 0 ) && m_currentWeapon.m_weaponFireMode == WeaponBase.fireModes.semiAuto )
        {
            m_currentWeapon.FireWeapon( );
        }
        else if( Input.GetMouseButton( 0 ) && m_currentWeapon.m_weaponFireMode == WeaponBase.fireModes.fullAuto )
        {
            m_currentWeapon.FireWeapon( );
        }

        if( Input.GetKeyDown(KeyCode.R) )
        {
            m_currentWeapon.ReloadWeapon( );
        }

    }
}
