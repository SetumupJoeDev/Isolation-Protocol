using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : ProjectileBase
{

    [Tooltip("An array containing the shards that are flung from the projectile on collision.")]
    public GameObject[] m_childShards;

    //The velocity modifier added to the child shards when they are flung
    private float m_velocityModifier = -1.75f;

    [Tooltip("A boolean that determines whether or not this projectile has yet fragmented into shards.")]
    public bool m_hasFragmented;

    public override void OnEnable( )
    {
        base.OnEnable( );

        //Loops through the projectiles shards, disables their colliders and sets their velocity equal to that of this projectile and resets their position
        foreach( GameObject shard in m_childShards )
        {
            
            shard.GetComponent<BoxCollider2D>( ).enabled = false;

            shard.GetComponent<IceProjectile>( ).m_projectileVelocity = m_projectileVelocity;

            shard.transform.position = transform.position;

        }

    }

    public override void ResetProjectile( )
    {
        base.ResetProjectile( );

        //Resets this boolean value to allow the projectile the fragment next time it is fired
        m_hasFragmented = false;

    }

    public override void CollideWithTarget( Collider2D collision )
    {
        
        base.CollideWithTarget( collision );

        //Stuns the enemy it collides with
        collision.GetComponent<CharacterBase>().Stun();

        //If the projectile hasn't yet fragmented, then it does so
        if ( !m_hasFragmented )
        {
            FragmentProjectile( );

            m_hasFragmented = true;
        }

    }

    public void FragmentProjectile( )
    {
        //Loops through the projectile's shards and fires them
        foreach ( GameObject shard in m_childShards )
        {

            //Un-childs the shards from their parent projectile so they can move freely
            shard.transform.SetParent( null );

            //Sets the shard's position to that of this projectile
            shard.transform.position = transform.position;

            //Activates the shard so it will be simulated
            shard.SetActive( true );

            IceProjectile shardScript = shard.GetComponent<IceProjectile>( );

            //Sets the previously damaged target of the current shard to that of this projectile so it will not immediately shatter
            shardScript.m_previouslyDamageTarget = m_previouslyDamageTarget;

            //Sets the velocity of the current shard using this projectile's velocity combined with the velocity modifier
            shardScript.m_projectileVelocity = new Vector2( m_projectileVelocity.x + m_velocityModifier , m_projectileVelocity.y );

            //Sets this to true so that the child shards to not themselves fragment
            shardScript.m_hasFragmented = true;

            //Enables collisions on the current shard
            shard.GetComponent<BoxCollider2D>( ).enabled = true;

            //Inverts the value of the velocity modifier to calculate the next shard's velocity correctly
            m_velocityModifier *= -1.0f;

        }
    }

}
