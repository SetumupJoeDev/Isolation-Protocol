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

    #region EnemyStates

    public enum enemyStates { idle, chasing, attacking };

    [Header("Enemy States")]
    [SerializeField]
    [Tooltip( "The current state the enemy is in." )]
    protected enemyStates m_currentState;

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
                    ChaseTarget( );
                }
                break;
            case ( enemyStates.attacking ):
                {

                }
                break;
        }

    }

    protected virtual void ChaseTarget( )
    {

        m_directionalVelocity = m_currentTarget.transform.position - transform.position;

        m_characterRigidBody.velocity = m_directionalVelocity.normalized * m_moveSpeed * Time.deltaTime;

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

    protected virtual IEnumerator CheckForTargets( )
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

    public void OnDrawGizmosSelected( )
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position , m_detectionRange );
    }

}
