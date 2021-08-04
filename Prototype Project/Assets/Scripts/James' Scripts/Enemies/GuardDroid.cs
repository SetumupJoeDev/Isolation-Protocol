using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDroid : EnemyBase
{
    public AudioClip m_taserZap;
    public override IEnumerator AttackTarget( )
    {
        AudioSource.PlayClipAtPoint(m_taserZap, transform.position);
        StartCoroutine(m_currentTarget.GetComponent<CharacterBase>( ).Stun( ) );

        return base.AttackTarget( );
    }

}
