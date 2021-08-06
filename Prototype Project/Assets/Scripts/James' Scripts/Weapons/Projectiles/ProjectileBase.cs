using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    #region Movement

    [Header("Movement")]

    [Tooltip("The directional velocity of the projectile.")]
    public Vector2 m_projectileVelocity;

    [Tooltip("The speed at which the projectile moves.")]
    public float m_projectileSpeed;

    [Tooltip("The lifetime of the projectile from the moment it is fired. If 0, the projectile will persist until destroyed externally or by damaging the max number of enemies.")]
    public float m_projectileLifetime;

    [Tooltip("The RigidBody attached to this projectile.")]
    public Rigidbody2D m_projectileRigidBody;

    private Transform m_parentTransform;

    #endregion

    [Space]

    #region Damage

    [Header("Damage")]
    [Tooltip("The amount of damage that this projectile will deal to enemies.")]
    public int m_projectileDamage;

    [Tooltip("The total number of enemies that this projectile can damage.")]
    public int m_maxDamagedTargets;

    [Tooltip("The current number of enemies that this projectile has damaged.")]
    public int m_currentDamagedTargets;

    [Tooltip("The previously damaged enemy. Used to prevent multiple damage calls on one enemy.")]
    public GameObject m_previouslyDamageTarget;

    public LayerMask m_wallLayer;

    #endregion


    public string m_firingWeaponName;

    // Start is called before the first frame update
    public virtual void OnEnable()
    {
        m_firingWeaponName = gameObject.transform.parent.transform.parent.name;

        if (analyticsEventManager.analytics != null)
        {
          

            analyticsEventManager.analytics.onBulletShoot(m_firingWeaponName); // When this object is spawned, gets the name of the gunthat shot it, and passes that name to analytics
           
        }

        //Assigns the rigidbody attached to this object as the projectileRigidBody
        m_projectileRigidBody = gameObject.GetComponent<Rigidbody2D>( );

        //If the projectile has a set lifetime, the WaitToDestroy coroutine is started
        if ( m_projectileLifetime != 0 )
        {
            StartCoroutine( WaitToDisable( m_projectileLifetime ) );
        }

        m_parentTransform = transform.parent; // maybe this messes with time reverse? 

        ResetProjectile( );

    }

    public virtual void ResetProjectile( )
    {
        m_currentDamagedTargets = 0;
        m_previouslyDamageTarget = null;
    }

    public virtual IEnumerator WaitToDisable( float lifetime )
    {
        //Waits for the duration of the projectile's lifetime before being destroyed
        yield return new WaitForSeconds( lifetime );

        DisableProjectile( );

    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
     

            CollideWithObject( collision );

       
       
    }

    public virtual void CollideWithObject( Collider2D collision )
    {
        //If the object the projectile collided with has a health manager and isn't the previously damaged object, then they take damage
        if ( collision.gameObject.GetComponent<HealthManager>( ) != null && collision.gameObject != m_previouslyDamageTarget )
        {
           

                
            
            CollideWithTarget( collision );

        }
        else if( LayerMask.LayerToName( collision.gameObject.layer ) == "Walls" || collision.gameObject.GetComponents<TutorialDummy>() != null && collision.gameObject != m_previouslyDamageTarget )
        {
            CollideWithWall( collision );
        }
        //Otherwise, the projectile is disabled
        else
        {
            Debug.LogWarning( "No logic written for current collision. " + collision.gameObject.name + " was hit by " + gameObject.name );
        }
    }

    public virtual void CollideWithTarget( Collider2D collision )
    {
        if (analyticsEventManager.analytics != null)
        {


            analyticsEventManager.analytics.onBulletHit(m_firingWeaponName); // Passes the name of what the bullet hit to analytics 
        }
        collision.gameObject.GetComponent<HealthManager>( ).TakeDamage( m_projectileDamage );
       

        

        //The value of currentDamagedTargets is incrememented to keep track of how many more objects this projectile can damage
        m_currentDamagedTargets++;

        //If this projectile has reached its target limit, it is destroyed
        if ( m_currentDamagedTargets >= m_maxDamagedTargets )
        {
            DisableProjectile( );
        }
        //Otherwise, the previouslyDamagedTarget is set as the object the projectile just collided with
        else
        {
            m_previouslyDamageTarget = collision.gameObject;
        }
    }

    public virtual void CollideWithWall( Collider2D collision )
    {
        DisableProjectile( );
    }

    public virtual void DisableProjectile( )
    {

        transform.SetParent( m_parentTransform ); // does this mess up the time rewind?

        gameObject.SetActive( false );

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves the projectile by its speed value in the direction of its velocity
        m_projectileRigidBody.velocity = m_projectileVelocity.normalized * m_projectileSpeed * Time.fixedDeltaTime;

        //Sets the upward transform of the projectile to its current velocity so that the projectile points in the direction it is moving
        transform.up = m_projectileRigidBody.velocity;

    }
}
