using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConventionalWeapon : GunBase
{

    #region Firing

    public enum fireModes { semiAuto, fullAuto };

    [Header("Firing")]
    [SerializeField]
    [Tooltip("The fire mode of the weapon.")]
    public fireModes m_weaponFireMode;

    [SerializeField]
    [Tooltip("The interval between each shot the weapon fires.")]
    protected float m_fireInterval;

    [SerializeField]
    [Tooltip("The projectile that the weapon will fire.")]
    protected GameObject m_projectilePrefab;

    [SerializeField]
    [Tooltip("The number of projectiles fired each time this weapon fires.")]
    protected int m_projectilesPerShot;

    #endregion

    private IEnumerator FireProjectiles( )
    {
        for ( int i = 0; i < m_projectilesPerShot; i++ )
        {
            //Instantiates a projectile at the barrel of the weapon 
            ProjectileBase newBullet = Instantiate( m_projectilePrefab, m_barrelTransform.position, Quaternion.identity ).GetComponent<ProjectileBase>();

            //Sets the projectiles velocity to the aiming direction of the weapon
            newBullet.m_projectileVelocity = m_aimingDirection;

            m_currentMagAmmo--;

            UpdateUIElements( );

            //Play's the weapon's firing sound
            m_fireSound.Play( );

            //Adds a short delay between firing each projectile, so they aren't all fired at once
            yield return new WaitForSeconds( m_fireInterval );

        }

        m_canWeaponFire = true;
    }

    public override void FireWeapon( )
    {
        if ( m_canWeaponFire && m_currentMagAmmo - m_projectilesPerShot >= 0 )
        {
            StartCoroutine( FireProjectiles( ) );

            m_canWeaponFire = false;

        }
    }

}
