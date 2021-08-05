using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : ProjectileBase
{

    public GameObject[] m_childShards;

    private float m_velocityModifier = -1.75f;

    public bool m_hasFragmented;

    public override void OnEnable( )
    {
        base.OnEnable( );

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

        m_hasFragmented = false;

    }

    public override void CollideWithTarget( Collider2D collision )
    {
        
        base.CollideWithTarget( collision );

        collision.GetComponent<CharacterBase>().Stun();

        if ( !m_hasFragmented )
        {
            FragmentProjectile( );

            m_hasFragmented = true;
        }

    }

    public void FragmentProjectile( )
    {
        foreach ( GameObject shard in m_childShards )
        {

            shard.transform.SetParent( null );

            shard.transform.position = transform.position;

            shard.SetActive( true );

            IceProjectile shardScript = shard.GetComponent<IceProjectile>( );

            shardScript.m_previouslyDamageTarget = m_previouslyDamageTarget;

            shardScript.m_projectileVelocity = new Vector2( m_projectileVelocity.x + m_velocityModifier , m_projectileVelocity.y );

            shardScript.m_hasFragmented = true;

            shard.GetComponent<BoxCollider2D>( ).enabled = true;

            //Inverts the value of the velocity modifier to calculate the next shard's velocity correctly
            m_velocityModifier *= -1.0f;

        }
    }

}
