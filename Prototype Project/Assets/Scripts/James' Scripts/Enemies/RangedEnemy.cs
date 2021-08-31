using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{

    [Header("Projectile Firing")]

    [Tooltip("The pooled array of projectiles belonging to the enemy.")]
    public GameObject[] m_projectileArray;

    [Tooltip("The position from which the enemy fires its projectiles.")]
    public Transform m_firingPosition;

    //The direction in which this enemy is aiming, usually towards its current target
    private Vector3 m_aimingDirection;

    [Space]

    [Header("Retreating")]

    [Tooltip("The distance the enemy has to be from the player before they begin to retreat.")]
    public float m_retreatProximity;

    [Tooltip("A boolean that determines whether or not the enemy is currently retreating.")]
    public bool m_isRetreating;

    public override IEnumerator AttackTarget( )
    {
        //Sets this to true so the coroutine cannot run more than once before being completed
        m_isAttacking = true;

        //Waits for the enemy's attack interval before continuing
        yield return new WaitForSeconds( m_attackInterval );

        //Fires a projectile at the current target
        FireProjectile( );

        //Sets this to false so the enemy can attack again
        m_isAttacking = false;

    }

    public override void AttackMode( )
    {
        //Runs the basic attack mode code
        base.AttackMode( );

        //If the player moves within the enemy's retreat proximity, the enemy begins to retreat
        if(Vector3.Distance(transform.position, m_currentTarget.transform.position) < m_retreatProximity )
        {
            m_isRetreating = true;
        }

    }

    protected override void FixedUpdate( )
    {

        //If the enemy is retreating, they move away from the player
        if ( m_isRetreating )
        {

            //Moves the player using negative speed to make them move backwards
            m_characterRigidBody.velocity = -m_aimingDirection.normalized * m_moveSpeed * Time.fixedDeltaTime;

            //If they are outside of retreat proximity, they stop retreating
            if( Vector3.Distance( transform.position , m_currentTarget.transform.position ) > m_retreatProximity )
            {
                m_isRetreating = false;
            }

        }
    }

    public void FireProjectile( )
    {
        //Calculates the aiming direction using the player's position and the enemy's position
        m_aimingDirection = m_currentTarget.transform.position - transform.position;

        //Loops through the projectile pool to find an inactive projectile to fire
        foreach( GameObject projectile in m_projectileArray )
        {
            //If an inactive projectile is found, it is positioned at the firing position, activated, and has its velocity set as the aiming direction calculated above
            if( !projectile.activeSelf )
            {
                projectile.transform.position = m_firingPosition.position;

                projectile.SetActive( true );

                projectile.GetComponent<ProjectileBase>( ).m_projectileVelocity = m_aimingDirection;

                //Breaks so that only one projectile is fired
                break;

            }
        }
    }

}
