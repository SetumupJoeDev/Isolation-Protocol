using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

    #region Movement

    [Header("Movement")]

    [SerializeField]
    private float m_moveSpeed;

    [SerializeField]
    private Vector3 m_moveDirection;

    [SerializeField]
    private float m_followDistance;

    private bool m_canFollow;

    [SerializeField]
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private GameObject m_playerObject;

    [Space]

    #endregion

    #region Combat

    [Header("Combat")]

    [SerializeField]
    private LayerMask m_targetLayer;

    [SerializeField]
    private float m_fireInterval;

    [SerializeField]
    private float m_fireRange;

    [SerializeField]
    private GameObject m_projectilePrefab;

    [SerializeField]
    private GameObject m_currentTarget;

    [SerializeField]
    private Vector3 m_firingDirection;

    [SerializeField]
    private bool m_canFire;

    [SerializeField]
    private bool m_isFiring;

    [Space]

    #endregion

    #region Drone Upgrades

    [Header("Drone Upgrades")]

    [SerializeField]
    private DroneBehaviourBase[] m_droneUpgrades;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Sets canFollow to true so that the drone follows the player from the start
        m_canFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If the drone is able to fire, isn't currently firing, and has enemies in range, the coroutine is started to delay firing
        if( m_canFire && !m_isFiring && EnemyInRange( ) )
        {
            StartCoroutine( WaitToFire( ) );
        }
    }

    public void DisableBasicBehaviours( )
    {
        //Stops all coroutines from being executed so that the drone no longer fires on enemies
        StopAllCoroutines( );
        //Prevents the drone from following the player or firing on enemies
        m_canFire = false;
        m_canFollow = false;
    }

    public void EnableBasicBehaviours( )
    {
        //Allows the drone to follow the player and fire upon enemies
        m_canFire = true;
        m_canFollow = true;
    }

    private IEnumerator WaitToFire( )
    {
        m_isFiring = true;

        //Waits for the fire interval to pass before firing a projectile at the enemy
        yield return new WaitForSeconds( m_fireInterval );

        FireAtEnemy( );

        m_isFiring = false;

    }

    private void FireAtEnemy( )
    {
        //Calculates an aiming direction based on the positions of the drone and the enemy
        m_firingDirection = m_currentTarget.transform.position - transform.position;

        //Instantiates a new projectile object and sets its directional velocity to be that calculated above
        ProjectileBase newProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBase>();

        newProjectile.m_projectileVelocity = m_firingDirection;

    }

    private void FixedUpdate( )
    {
        //If the drone is further from the player than the follow distance, it follows the player to move closer
        if ( Vector3.Distance( transform.position , m_playerObject.transform.position ) > m_followDistance && m_canFollow )
        {
            FollowPlayer( );
        }
        //Otherwise, its rigidbody's velocity is set to 0
        else
        {
            m_rigidBody.velocity = Vector3.zero;
        }
    }

    public bool EnemyInRange( )
    {

        //Uses an overlap circle to detect enemies within range
        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, m_fireRange, m_targetLayer);

        //If any enemies are detected, they are set as the current target and this method returns true
        if ( enemyCollider )
        {
            m_currentTarget = enemyCollider.gameObject;
            return true;
        }
        //Otherwise, it returns false
        else
        {
            return false;
        }
    }

    public void FollowPlayer( )
    {
        //Calculates the movement direction based on the positions of the drone and the player
        m_moveDirection = m_playerObject.transform.position - transform.position;

        //Sets the rigidbody's velocity to follow the player using the direction calculated above
        m_rigidBody.velocity = m_moveDirection * m_moveSpeed * Time.fixedDeltaTime;

    }

    public void DisableAllUpgrades( )
    {
        //Loops through all of the entries in the droneUpgrades array and disables them
        for(int i = 0; i < m_droneUpgrades.Length; i++ )
        {
            m_droneUpgrades[i].enabled = false;
        }
    }

}
