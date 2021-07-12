using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : ProjectileBase
{

    [Header("Harpoon Attributes")]

    public float m_lifetimeInWall;

    private int m_piercedEnemies;

    public Transform[] m_piercedEnemyPositions;

    public override void OnEnable( )
    {
        base.OnEnable( );

        //Sets the rigidbody to kinematic so it doesn't move on its own
        m_projectileRigidBody.isKinematic = false;

        //Freezes all of the rigidbody's constrains so it can't move or rotate
        m_projectileRigidBody.constraints = RigidbodyConstraints2D.None;

        m_piercedEnemies = 0;
    }

    public override void CollideWithTarget( Collider2D collision )
    {

        foreach( Transform child in m_piercedEnemyPositions )
        {
            if( collision.gameObject == child.gameObject )
            {
                return;
            }
        }

        base.CollideWithTarget( collision );

        collision.attachedRigidbody.isKinematic = true;

        collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        collision.gameObject.transform.SetParent( m_piercedEnemyPositions[ m_piercedEnemies ] );

        m_piercedEnemies++;

        collision.gameObject.transform.localPosition = Vector2.zero;

    }

    public override void CollideWithWall( Collider2D collision )
    {
        gameObject.transform.SetParent( collision.gameObject.transform );

        //Sets the rigidbody to kinematic so it doesn't move on its own
        m_projectileRigidBody.isKinematic = true;

        //Freezes all of the rigidbody's constrains so it can't move or rotate
        m_projectileRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine( WaitToDisable( m_lifetimeInWall ) );

    }

    public override void DisableProjectile( )
    {

        foreach( Transform child in m_piercedEnemyPositions )
        {

            if ( child.childCount != 0 )
            {
                GameObject currentChild = child.GetComponentInChildren<EnemyBase>().gameObject;

                if ( child.GetComponentInChildren<EnemyBase>( ) != null )
                {

                    currentChild.transform.SetParent( null );

                    Rigidbody2D childRigidBody = currentChild.gameObject.GetComponent<Rigidbody2D>();

                    childRigidBody.isKinematic = true;

                    childRigidBody.constraints = RigidbodyConstraints2D.None;

                }
            }
        }

        base.DisableProjectile( );
    }

}
