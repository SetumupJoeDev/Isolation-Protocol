using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDroid : EnemyBase
{

    [Tooltip("The sound that plays when the Guard Droid attacks.")]
    public AudioClip m_taserZap;

    public override IEnumerator AttackTarget( )
    {

        //Plays the enemy's attack sound
        AudioSource.PlayClipAtPoint(m_taserZap, transform.position);

        //Applies the stun status effect to the current target
        StartCoroutine(m_currentTarget.GetComponent<CharacterBase>( ).Stun( ) );

        //Runs the base attack code to damage the target
        return base.AttackTarget( );
    }

}
