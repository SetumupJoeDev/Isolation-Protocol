using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBase : CharacterBase
{

    #region TargetDetection

    [Header("Player Detection")]
    [Tooltip("The range at which this enemy can detect the player.")]
    public float m_detectionRange;

    [Tooltip("The layer on which the player sits. Used in target detection to filter to only the player.")]
    public LayerMask m_playerLayer;

    [Tooltip("The layer on which the game's walls sit. Used to determine if the enemy's vision is obscured.")]
    public LayerMask m_wallLayer;

    [Tooltip("The current target of this enemy.")]
    public GameObject m_currentTarget;

    [Tooltip("Determines whether or not the enemy is currently searching for targets.")]
    public bool m_searchingForTargets;

    [Tooltip("The interval between target searches. Used to save resources when lots of enemies are searching.")]
    public float m_searchInterval;

    #endregion

    #region Chasing

    [Header("Chasing")]
    [Tooltip("The closest distance to the player to which the enemy will move.")]
    public float m_chaseProximity;

    [Tooltip("The A* pathfinding component attached to this enemy that allows them to pathfind.")]
    public AIPath m_enemyAI;

    #endregion

    #region Attacking

    [Header("Attacking")]

    [Tooltip("The attack range of the enemy.")]
    public float m_attackRange;

    [Tooltip("The amount of damahe this enemy deals on each attack.")]
    public int m_attackDamage;

    [Tooltip("The interval between this enemy's attacks.")]
    public float m_attackInterval;

    [Tooltip("Determines whether or not the enemy is currently attacking.")]
    public bool m_isAttacking;

    [Tooltip("A boolean that determines whether or not this enemy can currently chase the player.")]
    public bool m_canChasePlayer;

    #endregion

    #region Death

    [Tooltip("The loot dropper script attached to this object.")]
    public LootDropper m_lootDropper;

    // Adam's Code

    [Tooltip("The EnemySpawner that spawned this enemy")]
    public EnemySpawner m_spawner;

    // End of Adam's Code

    #endregion

    #region EnemyStates

    public enum enemyStates { idle, chasing, attacking };

    [Header("Enemy States")]

    [Tooltip( "The current state the enemy is in." )]
    protected enemyStates m_currentState;

    #endregion

    #region Animation

    [Header("Animation")]

    [Tooltip("The animator component attached to this enemy.")]
    public Animator m_animator;

    #endregion

    #region Sounds

    [Header("Sounds")]

    [Tooltip("The sound that plays when the enemy dies.")]
    public AudioClip m_deathSound;

    #endregion


    private void Start()
    {
        m_enemyAI = GetComponent<AIPath>( );

        m_animator = GetComponent<Animator>( );

        //gameEvents.hello.enemyTargetPlayer += checkCanChasePlayer;
    }




    // Update is called once per frame
    protected override void Update()
    {
        //Switches through the enemy's current state and acts accordingly
        switch ( m_currentState )
        {
            //If the enemy is idle and not searching for targets, the search coroutine is executed
            case ( enemyStates.idle ):
                {
                    if( !m_searchingForTargets )
                    {
                        StartCoroutine( CheckForTargets( ) );
                        m_searchingForTargets = true;
                    }
                    
                }
                break;
            case ( enemyStates.chasing ):
                {
                    //If the enemy is in the chasing state and isn't currently knocked back, they chase the player
                    if ( m_currentState == enemyStates.chasing && !m_knockedBack )
                    {
                        ChaseTarget( );
                    }
                    //If the enemy is knocked back, their knockback force is simulated
                    if ( m_knockedBack )
                    {
                        SimulateKnockback( );
                    }
                }
                break;
                //If the enemy is in the attacking state, they attack the player or return to the chasing state
            case ( enemyStates.attacking ):
                {
                    AttackMode( );
                }
                break;
        }

        //Animates the enemy to face the correct direction using the correct animation
        Animate( );

        base.Update( );

    }

    protected void Animate( )
    {
        //Only calls this code if the enemy has found a target as otherwise they don't need to be animated due to being stationary
        if ( m_enemyAI.destination != null)
        {
            //Sets the value of the animator floats using the velocity of the enemy, so that the face the correct direction
            m_animator.SetFloat("Horizontal", m_enemyAI.desiredVelocity.normalized.x);
            m_animator.SetFloat("Vertical", m_enemyAI.desiredVelocity.normalized.y);
            m_animator.SetFloat("Speed", m_enemyAI.velocity.magnitude);
        }
    }

    public virtual void AttackMode( )
    {
        //If the player is outside of the enemy's attack range, they return to the chasing state
        if ( Vector3.Distance( transform.position , m_currentTarget.transform.position ) > m_attackRange ) 
        {
            m_enemyAI.canMove = true;
            
            m_currentState = enemyStates.chasing;
        }
        //If the enemy is not attacking, their attack coroutine is started
        else if ( !m_isAttacking && !m_isStunned)
        {
            
            StartCoroutine( AttackTarget( ) );
            m_isAttacking = true;
        }
        //If the player is no longer alive, the enemy returns to its idle state
        if ( m_currentTarget.GetComponent<PlayerHealthManager>( ).m_currentPlayerState != PlayerHealthManager.playerState.alive )
        {
            m_currentTarget = null;

            m_currentState = enemyStates.idle;

            m_searchingForTargets = false;

        }
    }


    //Lewis' code
    public void checkCanChasePlayer()
    {
        m_canChasePlayer = true;
    }
    //end of Lewis' code


    public override IEnumerator Stun( )
    {

        //Disables movement on the enemy's AI path so they can no longer chase the player
        m_enemyAI.canMove = false;

        //Enables the enemy's stun icon to give a visual indication of the status effect
        m_stunIcon.SetActive( true );

        //Waits for the stun duration before reversing the effects
        yield return new WaitForSeconds( m_stunDuration );

        //Disables the stun icon to inform the player that the enemy is no longer stunned
        m_stunIcon.SetActive(false);

        //Re-enables movement on the AI path so thr enemy can chase the player again
        m_enemyAI.canMove = true;

    }

    public override void Die( )
    {
        analyticsEventManager.analytics?.onEnemyDeath(gameObject.name);
        //Plays the enemy's death sound
        AudioSource.PlayClipAtPoint(m_deathSound, transform.position);

        //Drops a random selection of loot/currency
        m_lootDropper.DropLoot( );

        // Adam's Code
        if ( m_spawner != null )
        {
            m_spawner.IncreaseDead( gameObject );  
        }
        //End of Adam's code

        base.Die( );
    }

    protected virtual void ChaseTarget( )
    {

        //Calculates the current distance between this enemy and its target
        float distanceToTarget = Vector3.Distance( transform.position , m_currentTarget.transform.position );

        //Otherwise, if they are closer than the chase proximity, they enter the attacking state and have their velocity set to 0
        if( distanceToTarget <= m_chaseProximity )
        {
            m_currentState = enemyStates.attacking;
            m_enemyAI.canMove = false;

        }
        //If the player is further away than double the enemy's detection range, they lose sight of them and return to idle and have their velocity set to 0
        if ( distanceToTarget > m_detectionRange * 2 )
        {
            m_currentState = enemyStates.idle;
            m_searchingForTargets = false;
        }
    }

    protected virtual bool VisionObscured( Collider2D collider )
    {
        //Calculates the direction of the ray based on the positions of the enemy and the player
        Vector3 direction = collider.gameObject.transform.position - transform.position;

        //Calculates the distance between the enemy and its target for use in the raycast
        float distance = Vector3.Distance( transform.position, collider.gameObject.transform.position );

        //Sends out a raycast from the position of the enemy to that of the player, checking for any colliders on the wall layer
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction , distance, m_wallLayer );

        //If raycast hits a wall, then rayHit will not be null and the enemy's vision is obscured, so the method returns true
        if( rayHit.collider != null )
        {
            return true;
        }
        //Otherwise, the raycast has not hit a wall and the enemy can see the player, so the method returns false
        else
        {
            return false;
        }
    }

    public virtual IEnumerator CheckForTargets( )
    {

        //Uses an overlap circle to search for objects on the player layer within the detection range
        Collider2D collider = Physics2D.OverlapCircle( transform.position , m_detectionRange , m_playerLayer );

        //If the overlap circle finds a target, and the enemy's vision is not obscured, then the enemy enters chase mode and ceasing searching for targets
        if ( collider != null && !VisionObscured( collider ) && collider.GetComponent<PlayerHealthManager>().m_currentPlayerState == PlayerHealthManager.playerState.alive )
        {
            m_currentTarget = collider.gameObject;
            GetComponent<AIDestinationSetter>().target = m_currentTarget.transform;
            m_currentState = enemyStates.chasing;
            m_searchingForTargets = false;
            yield break;
        }
        //Otherwise, if the overlap circle did not find a target or the enemy's vision was obscured, then the coroutine waits before running again
        else
        {
            yield return new WaitForSeconds( m_searchInterval );
            m_searchingForTargets = false;
        }
    }

    public virtual IEnumerator AttackTarget( )
    {
        //Damages the player using the value of attack damage
        m_currentTarget.GetComponent<HealthManager>( ).TakeDamage( m_attackDamage );

        //Waits for the duration of the attack interval before being able to attack again
        yield return new WaitForSecondsRealtime( m_attackInterval );

        //IsAttacking is set to false so the enemy can attack again
        m_isAttacking = false;

    }
}
