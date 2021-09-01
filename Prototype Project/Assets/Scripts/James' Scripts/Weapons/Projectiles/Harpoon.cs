using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : ProjectileBase
{

    [Header("Harpoon Attributes")]

    [Tooltip("The amount of time after hitting a wall before this harpoon is destroyed.")]
    public float m_lifetimeInWall;

    [Tooltip("The number of enemies this harpoon has currently pierced.")]
    private int m_piercedEnemies;

    [Tooltip("An array of transforms, the positions of which pierced enemies will be placed upon.")]
    public Transform[] m_piercedEnemyPositions;

    public override void OnEnable( )
    {
        base.OnEnable( );

        //Sets the rigidbody to kinematic so it doesn't move on its own
        m_projectileRigidBody.isKinematic = false;

        //Freezes all of the rigidbody's constrains so it can't move or rotate
        m_projectileRigidBody.constraints = RigidbodyConstraints2D.None;

        //Resets the number of pierced enemies to 0
        m_piercedEnemies = 0;
    }

    public override void CollideWithTarget( Collider2D collision )
    {

        //Loops through all of the currently pierced enemies, and if they match the object collided with then the method returns
        foreach( Transform child in m_piercedEnemyPositions )
        {
            if( collision.gameObject == child.gameObject )
            {
                return;
            }
        }

        //Runs the base collision code, passing in the object collided with
        base.CollideWithTarget( collision );

        //Disables the physics simulation on the the enemy
        collision.attachedRigidbody.isKinematic = true;

        collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        //Childs the enemy to the piercing position at the index of piercedEnemies
        collision.gameObject.transform.SetParent( m_piercedEnemyPositions[ m_piercedEnemies ] );

        //Disables movement in the enemy's AI path
        collision.GetComponent<EnemyBase>( ).m_enemyAI.canMove = false;

        //Increments the value of piercedEnemies to keep track of the number of enemies currently skewered by the harpoon
        m_piercedEnemies++;

        //Resets the enemy's position to the origin of the pierce position it is childed to
        collision.gameObject.transform.localPosition = Vector2.zero;

    }

    public override void CollideWithWall( Collider2D collision )
    {

        //Childs the harpoon to the wall it hit
        gameObject.transform.SetParent( collision.gameObject.transform );

        //Sets the rigidbody to kinematic so it doesn't move on its own
        m_projectileRigidBody.isKinematic = true;

        //Freezes all of the rigidbody's constrains so it can't move or rotate
        m_projectileRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        //Starts the coroutine to destroy the harpoon after some time
        StartCoroutine( WaitToDisable( m_lifetimeInWall ) );

    }

    public override void DisableProjectile( )
    {
        //Loops through each enemy position on the harpoon
        foreach( Transform child in m_piercedEnemyPositions )
        {
            //If the pierce point has a childed object, then it is freed from the harpoon
            if ( child.childCount != 0 )
            {
                GameObject currentChild = child.GetComponentInChildren<EnemyBase>().gameObject;

                if ( child.GetComponentInChildren<EnemyBase>( ) != null )
                {
                    //Un-childs the enemy from the harpoon so it is not disabled with it
                    currentChild.transform.SetParent( null );

                    //Re-enables simulation of the enemy's rigidbody
                    Rigidbody2D childRigidBody = currentChild.gameObject.GetComponent<Rigidbody2D>();

                    childRigidBody.isKinematic = true;

                    childRigidBody.constraints = RigidbodyConstraints2D.None;

                    //Enables movement on the enemy's AI path
                    child.GetComponent<EnemyBase>( ).m_enemyAI.canMove = true;

                }
            }
        }

        //Runs the base method for disabling a projectile
        base.DisableProjectile( );
    }

}
