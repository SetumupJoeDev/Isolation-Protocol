using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{

    public override void TakeDamage( float damage )
    {
        base.TakeDamage( damage );
        if( m_currentHealth <= 0 )
        {
            Destroy( gameObject );
        }
    }

}
