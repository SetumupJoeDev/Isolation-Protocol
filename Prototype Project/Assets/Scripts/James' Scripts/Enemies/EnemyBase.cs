using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{

    #region TargetDetection

    [Header("Player Detection")]
    [SerializeField]
    [Tooltip("The range at which this enemy can detect the player.")]
    protected float m_detectionRange;

    [SerializeField]
    [Tooltip("The layer on which the player sits. Used in target detection to filter to only the player.")]
    protected LayerMask m_playerLayer;

    [SerializeField]
    [Tooltip("The layer on which the game's walls sit. Used to determine if the enemy's vision is obscured.")]
    protected LayerMask m_wallLayer;

    [SerializeField]
    [Tooltip("The current target of this enemy.")]
    protected GameObject m_currentTarget;

    [SerializeField]
    [Tooltip("Determines whether or not the enemy is currently searching for targets.")]
    protected bool m_searchingForTargets;

    [SerializeField]
    [Tooltip("The interval between target searches. Used to save resources when lots of enemies are searching.")]
    protected float m_searchInterval;

    #endregion

    #region Chasing

    [Header("Chasing")]
    [SerializeField]
    [Tooltip("The closest distance to the player to which the enemy will move.")]
    protected float m_chaseProximity;

    #endregion

    #region Attacking

    [Header("Attacking")]

    [SerializeField]
    [Tooltip("The attack range of the enemy.")]
    protected float m_attackRange;

    [SerializeField]
    [Tooltip("The amount of damahe this enemy deals on each attack.")]
    protected int m_attackDamage;

    [SerializeField]
    [Tooltip("The interval between this enemy's attacks.")]
    protected float m_attackInterval;

    [SerializeField]
    [Tooltip("Determines whether or not the enemy is currently attacking.")]
    protected bool m_isAttacking;

    #endregion

    #region Loot

    [Header("Loot")]

    [Range(1, 3)]
    [Tooltip("The modifier bonus to any loot drops this enemy has. Has a base of one, but can be increased.")]
    public float m_lootBoostModifier;

    [Tooltip("The maximum number of cigarette packs this enemy will drop when killed.")]
    public int m_maxCigaretteDrops;

    [Tooltip("The minimum number of cigarette packs this enemy will drop when killed.")]
    public int m_minCigaretteDrops;

    [Tooltip("The prefab for the cigarette pack gameobject.")]
    public GameObject m_cigPackPrefab;

    [Tooltip("The maximum number of fabricator fuel tanks this enemy will drop when killed.")]
    public int m_maxFuelDrops;

    [Tooltip("The minimum number of fabricator fuel tanks this enemy will drop when killed.")]
    public int m_minFuelDrops;

    [Tooltip("The prefab for the cigarette pack gameobject.")]
    public GameObject m_fabricatorFuelPrefab;

    // Adam's Code

    [Tooltip("The EnemySpawner that spawned this enemy")]
    public EnemySpawner m_spawner;
    
    // End of Adam's Code

    #endregion

    #region EnemyStates

    public enum enemyStates { idle, chasing, attacking };

    [Header("Enemy States")]
    [SerializeField]
    [Tooltip( "The current state the enemy is in." )]
    protected enemyStates m_currentState;

    #endregion

    #region Animation

    [SerializeField]
    protected Animator m_animator;

    #endregion

    #region Sounds

    [Header("Sounds")]

    [Tooltip("The sound that plays when the enemy dies.")]
    public AudioClip m_deathSound;

    #endregion

    // Update is called once per frame
    void Update()
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
                    
                }
                break;
                //If the enemy is in the attacking state, they attack the player or return to the chasing state
            case ( enemyStates.attacking ):
                {
                    //If the player is outside of the enemy's attack range, they return to the chasing state
                    if( Vector3.Distance(transform.position, m_currentTarget.transform.position) > m_attackRange )
                    {
                        m_currentState = enemyStates.chasing;
                    }
                    //If the enemy is not attacking, their attack coroutine is started
                    else if( !m_isAttacking )
                    {
                        StartCoroutine( AttackTarget( ) );
                        m_isAttacking = true;
                    }

                    if(m_currentTarget.GetComponent<PlayerHealthManager>().m_currentPlayerState != PlayerHealthManager.playerState.alive )
                    {
                        m_currentTarget = null;

                        m_currentState = enemyStates.idle;

                        m_searchingForTargets = false;

                    }

                }
                break;
        }

        //Animates the enemy to face the correct direction using the correct animation
        Animate( );

    }

    protected void Animate( )
    {
        //Sets the value of the animator floats using the velocity of the enemy, so that the face the correct direction
        m_animator.SetFloat( "Horizontal" , m_directionalVelocity.normalized.x );
        m_animator.SetFloat( "Vertical" , m_directionalVelocity.normalized.y );
        m_animator.SetFloat( "Speed" , m_characterRigidBody.velocity.sqrMagnitude );
    }

    protected override void FixedUpdate( )
    {
        //If the enemy is in the chasing state and isn't currently knocked back, they chase the player
        if( m_currentState == enemyStates.chasing && !m_knockedBack )
        {
            ChaseTarget( );
        }
        //If the enemy is knocked back, their knockback force is simulated
        if ( m_knockedBack )
        {
            SimulateKnockback( );
        }
    }

    public override void Die( )
    {
        //Plays the enemy's death sound
        AudioSource.PlayClipAtPoint(m_deathSound, transform.position);

        //Drops a random selection of loot/currency
        DropLoot( );

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
        //Calculates the directional velocity of the enemy using the position of their current target and the enemy
        m_directionalVelocity = m_currentTarget.transform.position - transform.position;

        //Calculates the current distance between 
        float distanceToTarget = Vector3.Distance( transform.position , m_currentTarget.transform.position );

        //If the enemy is further from the player than the chase proximity, then they move towards the player
        if ( distanceToTarget > m_chaseProximity )
        {
            m_characterRigidBody.velocity = m_directionalVelocity.normalized * m_moveSpeed * Time.deltaTime;
        }
        //Otherwise, if they are closer than the chase proximity, they enter the attacking state and have their velocity set to 0
        else if( distanceToTarget <= m_chaseProximity )
        {
            m_currentState = enemyStates.attacking;
            m_characterRigidBody.velocity = Vector3.zero;
            m_characterRigidBody.angularVelocity = 0;
        }
        //If the player is further away than double the enemy's detection range, they lose sight of them and return to idle and have their velocity set to 0
        if ( distanceToTarget > m_detectionRange * 2 )
        {
            m_currentState = enemyStates.idle;
            m_searchingForTargets = false;
            m_characterRigidBody.velocity = Vector3.zero;
            m_characterRigidBody.angularVelocity = 0;
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

        Debug.DrawLine( transform.position , collider.gameObject.transform.position );

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

    public virtual void DropLoot( )
    {
        //If the enemy is set to drop more than 0 cigarettes as their maximum, a random number of them is dropped
        if ( m_maxCigaretteDrops > 0 )
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float cigsToDrop = Random.Range(m_minCigaretteDrops * m_lootBoostModifier, m_maxCigaretteDrops * m_lootBoostModifier);

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            cigsToDrop = Mathf.Round( cigsToDrop );

            //Loops for the number generated above to generate currency drops
            for ( int i = 0; i < cigsToDrop; i++ )
            {
                //Instantiates a new cigarette packet and saves the base class as a local variable for later use
                CurrencyBase newCigPack = Instantiate(m_cigPackPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                //Generates a random number between -100 and 100 to be used in adding force to the currency so that they are ejected from the enemy and scattered
                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                //Adds force using the numbers generated above to scatter the cigarettes
                newCigPack.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newCigPack.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }
        //If the enemy is set to drop more than 0 fuel as their maximum, a random number of them is dropped
        if ( m_maxFuelDrops > 0 )
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float fuelToDrop = Random.Range(m_minFuelDrops * m_lootBoostModifier, m_maxFuelDrops * m_lootBoostModifier);

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            fuelToDrop = Mathf.Round( fuelToDrop );

            //Loops for the number generated above to generate currency drops
            for ( int i = 0; i < fuelToDrop; i++ )
            {

                //Instantiates a new fabricator fuel and saves the base class as a local variable for later use
                CurrencyBase newFuelTank = Instantiate(m_fabricatorFuelPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                //Generates a random number between -100 and 100 to be used in adding force to the currency so that they are ejected from the enemy and scattered
                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                //Adds force using the numbers generated above to scatter the cigarettes
                newFuelTank.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newFuelTank.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }
    }
}
