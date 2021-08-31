using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutoSlug : EnemyBase
{

    #region Attacking

    [Header("Leaping")]

    [Tooltip("Determines how long the enemy waits before pouncing.")]
    public float m_pouncePrepDuration;

    [Tooltip("The amount of force with which the enemy leaps at the player.")]
    public float m_leapForce;

    //The direction in which the Muto Slug will leap
    private Vector3 m_leapDirection;

    [Tooltip("A boolean that determines whether or not the slug is currently leaping.")]
    public bool m_isLeaping;

    [Tooltip("The duration of the slug's leap attack.")]
    public float m_leapDuration;

    [Tooltip("Determines whether or not this enemy has attached to its target.")]
    public bool m_isAttachedToTarget;

    [Tooltip("The integer value referring to the Physics Layer that this object sits on when it attaches to the player.")]
    public int m_attachedLayerIndex;

    [Tooltip("The player that this slug is attached to.")]
    public PlayerController m_attachedPlayer;

    #endregion

    #region Debuffs

    [Header("Debuffs")]

    [Range(-100, 0)]
    [Tooltip("The value by which the player's slowness debuff is changed when a MutoSlug attaches to them.")]
    public float m_playerSpeedDebuff;

    #endregion

    #region Collisions

    [Header("Collisions")]

    [Tooltip("The collider used to detect physical collisions and prevent the enmy from passing through walls.")]
    public GameObject m_colliderObject;

    #endregion

    protected override void FixedUpdate( )
    {
        //If the slug is currently leaping, the LeapAtPlayer method is executed to simulate the leaping physics
        if ( m_isLeaping )
        {
            LeapAtPlayer( );
        }
    }

    public override IEnumerator AttackTarget( )
    {
        if ( !m_isLeaping )
        {
            //If the enemy is not attached to its target, it will attempt to pounce on them and latch on
            if ( !m_isAttachedToTarget )
            {


                //Calculates the leap direction before "charging" the pounce, so that the player is able to dodge it
                m_leapDirection = m_currentTarget.transform.position - transform.position;

                //"Charges" the dodge to telegraph the attack to the player
                yield return new WaitForSeconds( m_pouncePrepDuration );

                //Sets this to true so that the leap attack will be simulated
                m_isLeaping = true;

                //Waits for the duration of the Muto SLug's leap before disabling the attack
                yield return new WaitForSeconds( m_leapDuration );

                //Sets this to false to return the slug to its normal behaviour
                m_isLeaping = false;

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
    }

    private void AttachToPlayer( Collider2D playerCollider )
    {
        //Sets the attachedPlayer to be the one passed in through parameters
        m_attachedPlayer = playerCollider.gameObject.GetComponent<PlayerController>( );

        //If the player hasn't reached the limit of attached slugs, this slug attaches to it
        if ( m_attachedPlayer.AttachNewSlug( gameObject ) )
        {
            //Adds the value of slowness to the player's speed debuff to slow their movement
            m_attachedPlayer.m_slowness += m_playerSpeedDebuff;

            //Sets the rigidbody to kinematic so it doesn't move on its own
            m_characterRigidBody.isKinematic = true;

            //Freezes all of the rigidbody's constrains so it can't move or rotate
            m_characterRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            //Sets this to true so that it can attack while attached to the player
            m_isAttachedToTarget = true;

            //Sets the enemy to be invulnerable so it can't be killed by the player unless they dodge into a wall
            m_healthManager.m_isVulnerable = true;

            //Sets the enemy's physics layer to 13, so that it doesn't collide with the player's projectiles
            gameObject.layer = 13;

            //Sets the slug's position to 0 so that it stays on the attachment point
            transform.position.Set( 0 , 0 , 0 );

            //Sets the slug's sorting order to 5 to render it above the player
            GetComponent<SpriteRenderer>( ).sortingOrder = 5;

            //Destroys the slug's wall collider to prevent it from blocking the player's movement
            Destroy( m_colliderObject );
        }

    }

    public void LeapAtPlayer( )
    {
        //Leaps towards the player using the leap force
        m_characterRigidBody.velocity = m_leapDirection * m_leapForce * Time.deltaTime;
    }

    public override void Die( )
    {
        if( m_isAttachedToTarget )
        {
            //Removes the value of the slowness debuff from the player if it was attached
            m_attachedPlayer.m_slowness -= m_playerSpeedDebuff;
        }

        base.Die();
    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        //If the slug collides with the player, it tries to attach to them
        if( m_currentState == enemyStates.attacking && collision.gameObject.tag == "Player" && !m_isAttachedToTarget )
        {
            AttachToPlayer( collision );
        }
    }

}
