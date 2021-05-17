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
        //Sets the rigidBody variable to be the rigidbody attached to the gameObject
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
        //Calculates the direction in which to move so that this object can gravitate towards the player
        Vector3 direction = m_playerObject.transform.position - transform.position;

        //Sets the rigidbody's velocity so that this object will gravitate towards the player, avoiding the need for the player to collect them manually
        m_rigidBody.velocity = direction.normalized * m_gravitationalSpeed * Time.fixedDeltaTime;

    }

    public virtual void CheckIsPlayerInRange( )
    {
        //Creates an overlap circle of a set radius to determine whether or not the player is currently in range
        Collider2D player = Physics2D.OverlapCircle(transform.position, m_gravitationRange, m_playerLayer );

        //If the player is found, then the object is saved as a variable for use in direction calculations and playerEnteredRange is set to true so that GravitateToPlayer will be called
        if( player != null && player.gameObject.GetComponent<PlayerController>() )
        {
            m_playerObject = player.gameObject;
            m_playerEnteredRange = true;
        }

    }

}
