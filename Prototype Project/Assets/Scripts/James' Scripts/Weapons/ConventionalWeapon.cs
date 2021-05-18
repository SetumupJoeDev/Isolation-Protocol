﻿using System.Collections;
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
    [Tooltip("The delay between projectiles being fired in burst mode.")]
    protected float m_burstFireDelay;

    [SerializeField]
    [Tooltip("The projectile that the weapon will fire.")]
    protected GameObject m_projectilePrefab;

    [SerializeField]
    [Tooltip("The number of projectiles fired each time this weapon fires. Should be left at 0 if this is not a burst fire weapon.")]
    protected uint m_projectilesPerShot;

    [Tooltip("The muzzle flash particle system attached to this weapon.")]
    public ParticleSystem m_muzzleFlash;

    #endregion

    private IEnumerator FireProjectiles( )
    {
        for ( int i = 0; i < m_projectilesPerShot; i++ )
        {
            //Instantiates a projectile at the barrel of the weapon 
            ProjectileBase newBullet = Instantiate( m_projectilePrefab, m_barrelTransform.position, Quaternion.identity ).GetComponent<ProjectileBase>();

            //Sets the projectiles velocity to the aiming direction of the weapon
            newBullet.m_projectileVelocity = m_aimingDirection;

            //Sets the new projectile's physics layer to 9, the PlayerProjectile layer, so that it will collide with enemies but not the player
            newBullet.gameObject.layer = 9;

            //Reduces the current ammo in the magazine by one
            m_currentMagAmmo--;

            //Updates the UI elements to reflect the new ammo levels
            UpdateUIElements( );

            //Play's the weapon's firing sound
            m_fireSound.Play( );

            //Plays the muzzle flash visual effects
            m_muzzleFlash.Play( );

            //Waits for the duration of the burst fire delay before firing again
            yield return new WaitForSeconds( m_burstFireDelay );

        }

        //Adds a short delay between firing each projectile, so they aren't all fired at once
        yield return new WaitForSeconds( m_fireInterval );

        //Sets this to true so that the weapon can fire again
        m_canWeaponFire = true;
    }

    public override void FireWeapon( )
    {
        //If the weapon can fire and has sufficient ammo to do so, the firing coroutine is started
        if ( m_canWeaponFire && m_currentMagAmmo - m_projectilesPerShot >= 0 )
        {
            StartCoroutine( FireProjectiles( ) );

            m_canWeaponFire = false;

        }
    }

}
