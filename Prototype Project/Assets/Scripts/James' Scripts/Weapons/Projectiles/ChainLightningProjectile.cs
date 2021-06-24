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

    public override void OnEnable( )
    {
        base.OnEnable( );

        //Sets the length of the previouslyDamagedEnemies array to the maximum number of enemies this projectile can damage
        m_previouslyDamagedEnemies = new GameObject[m_maxDamagedEnemies];

    }

    public override void CollideWithObject( Collider2D collision )
    {
        base.CollideWithObject( collision );

        //If the projectile is not leaping between enemies as it has not yet hit one, the enemy hit is set as the first in the array and the projectile begins leaping between enemies
        if ( !m_leapingBetweenEnemies )
        {
            m_previouslyDamagedEnemies[0] = collision.gameObject;

            m_leapingBetweenEnemies = true;

        }

        //Finds a new target to leap to
        FindLeapTarget( );

    }

    public void FindLeapTarget( )
    {
        //Uses an overlap circle to find any enemies within a range
        Collider2D[] foundTargets = Physics2D.OverlapCircleAll(transform.position, m_leapRange, m_enemyLayer );

        //If the circle finds an enemy and it is not the previously damaged enemy, or if it finds more than one enemy, it filters to find the closest
        if ( foundTargets.Length == 1 && foundTargets[0].gameObject != m_previouslyDamageEnemy || foundTargets.Length > 1 ) 
        {
            FilterPreviousTargets( foundTargets ); 
        }
        //Otherwise, if no more enemies are found, the projectile is destroyed
        else
        {
            Destroy( gameObject );
        }

    }

    private void FilterPreviousTargets( Collider2D[] foundTargets )
    {

        //Creates a new list which will be the refined list of targets
        List<GameObject> refinedTargetList = new List<GameObject>();

        //Loops through the array of targets passed in and checks if they have been previously damaged
        foreach ( Collider2D collider in foundTargets )
        {

            bool wasPreviouslyTargeted = false;

            //Loops through the array of previously damaged enemies, checking if they are the current enemy in the array passed in
            foreach ( GameObject enemy in m_previouslyDamagedEnemies )
            {

                if ( collider.gameObject == enemy )
                {
                    wasPreviouslyTargeted = true;
                }
            }

            //If the current enemy wasn't previously targeted by the projectile, it is added to the list
            if ( !wasPreviouslyTargeted )
            {
                refinedTargetList.Add( collider.gameObject );
            }

        }

        //If the list has one entry that isn't the previously damaged enemy, or there are multiple targets, then the closest target is found
        if ( refinedTargetList.Count == 1 && refinedTargetList[0] != m_previouslyDamageEnemy || refinedTargetList.Count > 1 )
        {
            FindClosestTarget( refinedTargetList );
        }

    }

    private void FindClosestTarget( List<GameObject> refinedTargetList )
    {
        //Creates an array of floats to store the distances between the projectile and each enemy
        float[] distanceArray = new float[refinedTargetList.Count]; 

        //Loops through the list of refined targets and saves the distance between them and the projectile to the new array
        for( int i = 0; i < refinedTargetList.Count; i++ )
        {
            distanceArray[i] = Vector3.Distance( transform.position , refinedTargetList[i].transform.position );
        }

        //Creates a new index to keep track of the closest target
        int closestTargetIndex = 0;

        float closestTarget = distanceArray[closestTargetIndex];

        //Loops through the distance array comparing each to the previous distance
        for( int j = 1; j < distanceArray.Length; j++ )
        {
            //If the current distance is less than the closest target, it is set as the new closest target and the index is saved
            if( distanceArray[j] < closestTarget )
            {
                closestTarget = distanceArray[j];

                closestTargetIndex = j;

            }
        }

        //Sets the projectile's velocity using the position of the newly found closest enemy and the position of the projectile
        m_projectileVelocity = refinedTargetList[closestTargetIndex].transform.position - transform.position;

    }

}
