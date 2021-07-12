using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDroid : EnemyBase
{

    public override IEnumerator AttackTarget( )
    {

        StartCoroutine(m_currentTarget.GetComponent<CharacterBase>( ).Stun( ) );

        return base.AttackTarget( );
    }

}
