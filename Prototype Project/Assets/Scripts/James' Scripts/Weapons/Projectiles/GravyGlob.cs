using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravyGlob : ProjectileBase
{

    [Tooltip("The prefab for the gravy puddle this projectile leaves behind.")]
    public GameObject m_gravyPuddlePrefab;

    public override void DisableProjectile( )
    {

        //Instantiates a gravy puddle at the position of the projectile
        Instantiate( m_gravyPuddlePrefab , transform.position , Quaternion.identity );

        //Runs the base version of the DisableProjectile method
        base.DisableProjectile( );
    }

}
