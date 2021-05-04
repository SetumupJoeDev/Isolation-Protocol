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

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {

        switch ( m_currentState )
        {
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
            case ( enemyStates.attacking ):
                {
                    if( Vector3.Distance(transform.position, m_currentTarget.transform.position) > m_attackRange )
                    {
                        m_currentState = enemyStates.chasing;
                    }
                    else if( !m_isAttacking )
                    {
                        StartCoroutine( AttackTarget( ) );
                        m_isAttacking = true;
                    }
                }
                break;
        }

        Animate( );

    }

    protected void Animate( )
    {
        m_animator.SetFloat( "Horizontal" , m_directionalVelocity.normalized.x );
        m_animator.SetFloat( "Vertical" , m_directionalVelocity.normalized.y );
        m_animator.SetFloat( "Speed" , m_characterRigidBody.velocity.sqrMagnitude );
    }

    protected override void FixedUpdate( )
    {
        if( m_currentState == enemyStates.chasing )
        {
            ChaseTarget( );
        }
    }

    public override void Die( )
    {
        AudioSource.PlayClipAtPoint(m_deathSound, transform.position);
        DropLoot( );
        m_spawner.IncreaseDead(gameObject);  // Adam's Code
        base.Die( );
    }

    protected virtual void ChaseTarget( )
    {

        m_directionalVelocity = m_currentTarget.transform.position - transform.position;

        float distanceToTarget = Vector3.Distance( transform.position , m_currentTarget.transform.position );

        if ( distanceToTarget > m_chaseProximity )
        {
            m_characterRigidBody.velocity = m_directionalVelocity.normalized * m_moveSpeed * Time.deltaTime;
        }
        else if( distanceToTarget <= m_chaseProximity )
        {
            m_currentState = enemyStates.attacking;
            m_characterRigidBody.velocity = Vector3.zero;
            m_characterRigidBody.angularVelocity = 0;
        }

        if ( distanceToTarget > m_detectionRange * 2 )
        {
            m_currentState = enemyStates.idle;
            m_searchingForTargets = false;
            m_characterRigidBody.velocity = Vector3.zero;
            m_characterRigidBody.angularVelocity = 0;
        }

        Debug.DrawRay( transform.position , m_directionalVelocity );

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
        if ( collider != null && !VisionObscured( collider ) )
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
        m_currentTarget.GetComponent<HealthManager>( ).TakeDamage( m_attackDamage );

        yield return new WaitForSecondsRealtime( m_attackInterval );

        m_isAttacking = false;

    }

    public virtual void DropLoot( )
    {
        if ( m_maxCigaretteDrops > 0 )
        {
            float cigsToDrop = Random.Range(m_minCigaretteDrops * m_lootBoostModifier, m_maxCigaretteDrops * m_lootBoostModifier);

            cigsToDrop = Mathf.Round( cigsToDrop );

            for ( int i = 0; i < cigsToDrop; i++ )
            {
                Debug.Log( "Dropped cigarette " + ( i + 1 ) + " of " + cigsToDrop );

                CurrencyBase newCigPack = Instantiate(m_cigPackPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                newCigPack.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newCigPack.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }
        if( m_maxFuelDrops > 0 )
        {
            float fuelToDrop = Random.Range(m_minFuelDrops * m_lootBoostModifier, m_maxFuelDrops * m_lootBoostModifier);

            fuelToDrop = Mathf.Round( fuelToDrop );

            for ( int i = 0; i < fuelToDrop; i++ )
            {
                Debug.Log( "Dropped fuel tank " + ( i + 1 ) + " of " + fuelToDrop );

                CurrencyBase newFuelTank = Instantiate(m_fabricatorFuelPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                newFuelTank.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newFuelTank.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }
    }

    public void OnDrawGizmosSelected( )
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position , m_detectionRange );
    }

}
