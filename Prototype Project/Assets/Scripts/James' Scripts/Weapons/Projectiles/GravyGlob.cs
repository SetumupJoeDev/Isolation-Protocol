using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravyGlob : ProjectileBase
{

    public GameObject m_gravyPuddlePrefab;

    public override void DisableProjectile( )
    {

        Instantiate( m_gravyPuddlePrefab , transform.position , Quaternion.identity );

        base.DisableProjectile( );
    }

}
