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
        
    }

    // Update is called once per frame
    void Update()
    {
        if( !m_isFiring && EnemyInRange( ) )
        {
            StartCoroutine( WaitToFire( ) );
        }
    }

    private IEnumerator WaitToFire( )
    {
        m_isFiring = true;

        yield return new WaitForSeconds( m_fireInterval );

        FireAtEnemy( );

        m_isFiring = false;

    }

    private void FireAtEnemy( )
    {
        m_firingDirection = m_currentTarget.transform.position - transform.position;

        ProjectileBase newProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBase>();

        newProjectile.m_projectileVelocity = m_firingDirection;

    }

    private void FixedUpdate( )
    {
        if ( Vector3.Distance( transform.position , m_playerObject.transform.position ) > m_followDistance )
        {
            FollowPlayer( );
        }
        else
        {
            m_rigidBody.velocity = Vector3.zero;
        }
    }

    public bool EnemyInRange( )
    {

        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, m_fireRange, m_targetLayer);

        if ( enemyCollider )
        {
            m_currentTarget = enemyCollider.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FollowPlayer( )
    {
        m_moveDirection = m_playerObject.transform.position - transform.position;

        m_rigidBody.velocity = m_moveDirection * m_moveSpeed * Time.fixedDeltaTime;

    }

    public void DisableAllUpgrades( )
    {
        for(int i = 0; i < m_droneUpgrades.Length; i++ )
        {
            m_droneUpgrades[i].enabled = false;
        }
    }

}
