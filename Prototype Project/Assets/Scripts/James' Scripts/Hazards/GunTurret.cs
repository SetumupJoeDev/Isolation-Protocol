using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : TurretBase
{

    public GameObject m_projectile;

    public Transform[] m_barrelTransforms;

    public override IEnumerator FireAtTarget( )
    {

        m_isFiring = true;

        foreach( Transform barrel in m_barrelTransforms )
        {
            ProjectileBase newProjectile = Instantiate( m_projectile , barrel.position , Quaternion.identity ).GetComponent<ProjectileBase>();

            newProjectile.m_projectileVelocity = m_aimingDirection;

            newProjectile.gameObject.layer = 15;

            m_firingSound.Play( );

            yield return new WaitForSeconds( m_fireInterval );

        }

        m_isFiring = false;

    }

}
