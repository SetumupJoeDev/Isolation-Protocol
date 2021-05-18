using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : TurretBase
{

    public GameObject m_projectile;

    public Transform[] m_barrelTransforms;

    public override IEnumerator FireAtTarget( )
    {

        //Sets isFiring to true to prevent the coroutine from being started more than once
        m_isFiring = true;

        //Loops through the number of barrel transforms in the array, firing a projectile from each
        foreach( Transform barrel in m_barrelTransforms )
        {
            //Instantiates a new projectile and saves its controller for later use
            ProjectileBase newProjectile = Instantiate( m_projectile , barrel.position , Quaternion.identity ).GetComponent<ProjectileBase>();

            //Sets the velocity of the projectile using the aiming direction previously calculated in the base class
            newProjectile.m_projectileVelocity = m_aimingDirection;

            //Sets the physics layer of the projectile to 15, enemy projectiles, so it can affect the player
            newProjectile.gameObject.layer = 15;

            //Plays the turret's firing sound
            m_firingSound.Play( );

            //Waits for the firing interval before firing again
            yield return new WaitForSeconds( m_fireInterval );

        }

        //Sets isFiring to false so that the coroutine can be called again
        m_isFiring = false;

    }

}
