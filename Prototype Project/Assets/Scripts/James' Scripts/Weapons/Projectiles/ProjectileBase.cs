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

    // Start is called before the first frame update
    public virtual void Start()
    {

        m_projectileRigidBody = gameObject.GetComponent<Rigidbody2D>( );

        if ( m_projectileLifetime != 0 )
        {
            StartCoroutine( WaitToDestroy( ) );
        }

    }

    public IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds( m_projectileLifetime );

        Destroy(gameObject);

    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        CollideWithObject( collision );
    }

    public virtual void CollideWithObject( Collider2D collision )
    {
        if ( collision.gameObject.GetComponent<HealthManager>( ) != null && collision.gameObject != m_previouslyDamageEnemy )
        {
            collision.gameObject.GetComponent<HealthManager>( ).TakeDamage( m_projectileDamage );

            m_currentDamagedEnemies++;

            if ( m_currentDamagedEnemies >= m_maxDamagedEnemies )
            {
                Destroy( gameObject );
            }
            else
            {
                m_previouslyDamageEnemy = collision.gameObject;
            }

        }
        else
        {
            Destroy( gameObject );
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_projectileRigidBody.velocity = m_projectileVelocity.normalized * m_projectileSpeed * Time.fixedDeltaTime;

        transform.up = m_projectileRigidBody.velocity;

    }
}
