using System.Collections;
using System.Collections.Generic;
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
    public int m_maxDamagedEnemies;

    [Tooltip("The current number of enemies that this projectile has damaged.")]
    public int m_currentDamagedEnemies;

    [Tooltip("The previously damaged enemy. Used to prevent multiple damage calls on one enemy.")]
    public GameObject m_previouslyDamageEnemy;

    #endregion





    enemyCounter  m_enemyCounter;
    // Start is called before the first frame update
    public virtual void OnEnable()
    {
        // if(parentGameObject !== enemy){ increment bullets, and pass the parentgameobject as a string into enemyCounter

        if ( GameObject.Find( "easyName" ).GetComponent<enemyCounter>( ) != null )
        {
            m_enemyCounter = GameObject.Find( "easyName" ).GetComponent<enemyCounter>( );
        }
        if (transform.parent.gameObject.transform.parent.tag == "Weapon")
        {
            m_enemyCounter.bulletCounter(1, transform.parent.gameObject.transform.parent.name);
        }
        //Assigns the rigidbody attached to this object as the projectileRigidBody
        m_projectileRigidBody = gameObject.GetComponent<Rigidbody2D>( );

        //If the projectile has a set lifetime, the WaitToDestroy coroutine is started
        if ( m_projectileLifetime != 0 )
        {
            StartCoroutine( WaitToDisable( ) );
        }

        m_parentTransform = transform.parent;

    }

    public IEnumerator WaitToDisable()
    {
        //Waits for the duration of the projectile's lifetime before being destroyed
        yield return new WaitForSeconds( m_projectileLifetime );

        transform.SetParent( m_parentTransform );

        gameObject.SetActive( false );

    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        CollideWithObject( collision );

        if (collision.gameObject.tag == "Enemy")
        {

            m_enemyCounter.bulletHitCounter(transform.parent.gameObject.transform.parent.name,collision.gameObject.name);
        }
       
    }

    public virtual void CollideWithObject( Collider2D collision )
    {
        //If the object the projectile collided with has a health manager and isn't the previously damaged enemy, then they take damage
        if ( collision.gameObject.GetComponent<HealthManager>( ) != null && collision.gameObject != m_previouslyDamageEnemy )
        {
            
            

            collision.gameObject.GetComponent<HealthManager>( ).TakeDamage( m_projectileDamage );
           

            //The value of currentDamagedEnemies is incrememented to keep track of how many more enemies this projectile can damage
            m_currentDamagedEnemies++;

            //If this projectile has reached its enemy limit, it is destroyed
            if ( m_currentDamagedEnemies >= m_maxDamagedEnemies )
            {
                transform.SetParent( m_parentTransform );

                gameObject.SetActive( false );
            }
            //Otherwise, the previouslyDamagedEnemy is set as the enemy the projectile just collided with
            else
            {
                m_previouslyDamageEnemy = collision.gameObject;
            }

        }
        //Otherwise, the projectile is disabled
        else
        {
            transform.SetParent( m_parentTransform );

            gameObject.SetActive( false );
        }
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
