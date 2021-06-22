using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{

    [Header("Projectile Firing")]

    public GameObject[] m_projectileArray;

    public Transform m_firingPosition;

    private Vector3 m_aimingDirection;

    public override IEnumerator AttackTarget( )
    {

        m_isAttacking = true;

        yield return new WaitForSeconds( m_attackInterval );

        FireProjectile( );

        m_isAttacking = false;

    }

    public void FireProjectile( )
    {

        m_aimingDirection = m_currentTarget.transform.position - transform.position;

        foreach( GameObject projectile in m_projectileArray )
        {
            if( !projectile.activeSelf )
            {
                projectile.transform.position = m_firingPosition.position;

                projectile.SetActive( true );

                projectile.GetComponent<ProjectileBase>( ).m_projectileVelocity = m_aimingDirection;

                break;

            }
        }
    }

}
