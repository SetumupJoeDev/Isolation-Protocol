using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningProjectile : ProjectileBase
{

    [Header("Enemy Leaping")]
    [Tooltip("An array of all the enemies this projectile has previously hit.")]
    public GameObject[] m_previouslyDamagedEnemies;

    [Tooltip("Determines whether or not the projectile is currently leaping between enemies, or if it is simply travelling.")]
    public bool m_leapingBetweenEnemies;

    [Tooltip("The range that the projectile can leap within.")]
    public float m_leapRange;

    [Tooltip("The physics layer that the enemies sit on, used to detect enemies to leap to.")]
    public LayerMask m_enemyLayer;

    public override void Start( )
    {
        base.Start( );

        m_previouslyDamagedEnemies = new GameObject[m_maxDamagedEnemies];

    }

    public override void CollideWithObject( Collider2D collision )
    {
        base.CollideWithObject( collision );

        if ( !m_leapingBetweenEnemies )
        {
            m_previouslyDamagedEnemies[0] = collision.gameObject;

            m_leapingBetweenEnemies = true;

        }

        FindLeapTarget( );

    }

    public void FindLeapTarget( )
    {

        Collider2D[] foundTargets = Physics2D.OverlapCircleAll(transform.position, m_leapRange, m_enemyLayer );

        if ( foundTargets.Length == 1 && foundTargets[0].gameObject != m_previouslyDamageEnemy || foundTargets.Length > 1 ) 
        {
            FilterPreviousTargets( foundTargets ); 
        }
        else
        {
            Destroy( gameObject );
        }

    }

    private void FilterPreviousTargets( Collider2D[] foundTargets )
    {
        List<GameObject> refinedTargetList = new List<GameObject>();

        foreach ( Collider2D collider in foundTargets )
        {

            bool wasPreviouslyTargeted = false;

            foreach ( GameObject enemy in m_previouslyDamagedEnemies )
            {

                if ( collider.gameObject == enemy )
                {
                    wasPreviouslyTargeted = true;
                }
            }

            if ( !wasPreviouslyTargeted )
            {
                refinedTargetList.Add( collider.gameObject );
            }

        }

        if ( refinedTargetList.Count == 1 && refinedTargetList[0] != m_previouslyDamageEnemy || refinedTargetList.Count > 1 )
        {
            FindClosestTarget( refinedTargetList );
        }

    }

    private void FindClosestTarget( List<GameObject> refinedTargetList )
    {

        float[] distanceArray = new float[refinedTargetList.Count]; 

        for( int i = 0; i < refinedTargetList.Count; i++ )
        {
            distanceArray[i] = Vector3.Distance( transform.position , refinedTargetList[i].transform.position );
        }

        int closestTargetIndex = 0;

        float closestTarget = distanceArray[closestTargetIndex];

        for( int j = 1; j < distanceArray.Length; j++ )
        {
            if( distanceArray[j] < closestTarget )
            {
                closestTarget = distanceArray[j];

                closestTargetIndex = j;

            }
        }

        m_projectileVelocity = refinedTargetList[closestTargetIndex].transform.position - transform.position;

    }

}
