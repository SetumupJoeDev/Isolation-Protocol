using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeBomber : EnemyBase
{

    [Header("Explosion")]

    [Tooltip("The radius of the explosion. Anything within this radius will take damage.")]
    public float m_explosionRadius;

    [Tooltip("The GameObject with an attached particle system that will play the spore explosion effect.")]
    public GameObject m_explosionParticles;

    [Tooltip("A boolean that determines whether or not this enemy is currently detonating.")]
    public bool m_isDetonating;

    public override IEnumerator AttackTarget( )
    {
        //Runs this enemy's death function, as they die when they attack
        Die( );

        //Returns null as this enemy can only attack once before it dies
        yield return null;

    }

    public override void Die( )
    {
        //If the enemy isn't currently detonating, the Detonate and base Die methods are executed
        if ( !m_isDetonating )
        {

            Detonate( );

            base.Die( );

        }
    }

    public void Detonate( )
    {

        //Sets this to true so the enemy can only detonate once
        m_isDetonating = true;

        //Sets the transform parent of the particle explosion to null so that the object isn't destroyed with the enemy itself
        m_explosionParticles.transform.SetParent( null );

        //Plays the explosion particle effect to visualise to the player that the enemy has exploded
        m_explosionParticles.GetComponent<ParticleSystem>( ).Play( );

        //Uses an overlap circle to return an array of Colliders that were within the range of the explosion
        Collider2D[] damagedEntities = Physics2D.OverlapCircleAll( transform.position, m_explosionRadius );

        //Loops through each collider, checking that they have a health manager before damaging them
        foreach( Collider2D entity in damagedEntities )
        {
            
            //Attempts to assign the health manager of the current entity to a local variable for future use
            HealthManager entityHealthManager = entity.GetComponent<HealthManager>( );

            //If after assignment the value is null, or the gameObject it attached to is this enemy, then the current entity can't take damage and is skipped
            if ( entityHealthManager != null && entityHealthManager.gameObject != gameObject )
            {
                //Otherwise, the entity can be damaged and takes damage equal to this enemy's attack damage value
                entityHealthManager.TakeDamage( m_attackDamage );
            }
        }
    }

}
