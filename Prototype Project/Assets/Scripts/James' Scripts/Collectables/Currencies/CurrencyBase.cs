using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyBase : CollectableBase
{

    #region Collection

    [Header("Collection")]

    [Tooltip("The player that has come into this object's range.")]
    public GameObject m_playerObject;

    [Tooltip("A boolean that determines whether or not the player has come within collection range of this object.")]
    public bool m_playerEnteredRange;

    [Tooltip("The physics layer on which the player sits.")]
    public LayerMask m_playerLayer;

    [Tooltip("The range in which the player must be to this object for it to gravitate towards them.")]
    public float m_gravitationRange;

    #endregion

    #region Gravitation

    [Header("Gravitation")]

    [Tooltip("The speed at which the object will gravitate towards its target.")]
    public float m_gravitationalSpeed;

    [Tooltip("The rigidbody of the object that the gravitation force will be applied to.")]
    public Rigidbody2D m_rigidBody;

    #endregion

    public void Start( )
    {
        m_rigidBody = GetComponent<Rigidbody2D>( );
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        if( !m_playerEnteredRange )
        {
            CheckIsPlayerInRange( );
        }

    }

    public virtual void FixedUpdate( )
    {
        if( m_playerEnteredRange )
        {
            GravitateToPlayer( );
        }
    }

    public virtual void GravitateToPlayer( )
    {
        Vector3 direction = m_playerObject.transform.position - transform.position;

        m_rigidBody.velocity = direction.normalized * m_gravitationalSpeed * Time.fixedDeltaTime;

    }

    public virtual void CheckIsPlayerInRange( )
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, m_gravitationRange, m_playerLayer );

        if( player != null )
        {
            m_playerObject = player.gameObject;
            m_playerEnteredRange = true;
        }

    }

}
