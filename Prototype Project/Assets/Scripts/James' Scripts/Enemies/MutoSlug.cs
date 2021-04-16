using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutoSlug : EnemyBase
{

    #region Attacking

    [Header("Attacking")]

    [Tooltip("Determines how long the enemy waits before pouncing.")]
    public float m_pouncePrepDuration;

    [Tooltip("The amount of force with which the enemy leaps at the player.")]
    public float m_leapForce;

    [Tooltip("Determines whether or not this enemy has attached to its target.")]
    public bool m_isAttachedToTarget;

    [Tooltip("The integer value referring to the Physics Layer that this object sits on when it attaches to the player.")]
    public int m_attachedLayerIndex;

    #endregion

    [Header("Collisions")]

    [Tooltip("The collider used to detect physical collisions and prevent the enmy from passing through walls.")]
    public GameObject m_colliderObject;

    public override IEnumerator AttackTarget( )
    {
        //If the enemy is not attached to its target, it will attempt to pounce on them and latch on
        if ( !m_isAttachedToTarget )
        {
            //Calculates the leap direction before "charging" the pounce, so that the player is able to dodge it
            Vector3 leapDirection = m_currentTarget.transform.position - transform.position;

            //"Charges" the dodge to telegraph the attack to the player
            yield return new WaitForSeconds( m_pouncePrepDuration );

            //Leaps towards the player using the leap force
            m_characterRigidBody.velocity = leapDirection * m_leapForce * Time.deltaTime;

        }
        else
        {
            //Waits until the attack interval has passed to deal damage so that the player doesn't take damage each frame
            yield return new WaitForSeconds( m_attackInterval );

            //Reduce's the target's health by the value of attack damage
            m_currentTarget.GetComponent<HealthManager>( ).TakeDamage( m_attackDamage );

        }

        //Sets this to false so that the coroutine can be executed again
        m_isAttacking = false;

    }

    private void AttachToPlayer( Collider2D playerCollider )
    {
        transform.parent = playerCollider.gameObject.transform;

        m_characterRigidBody.isKinematic = true;

        m_characterRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        m_isAttachedToTarget = true;

        m_healthManager.m_isInvulnerable = true;

        gameObject.layer = 13;

        Destroy( m_colliderObject );

    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        if( m_currentState == enemyStates.attacking && collision.gameObject.tag == "Player" )
        {
            AttachToPlayer( collision );
        }
    }

}
