using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamEnemy : EnemyBase
{

    #region Attacking

    [Header("Attacking")]

    [Tooltip("The duration of this enemy's attack windup.")]
    public float m_windupDuration;

    [Tooltip("The range of this enemy's attack effect.")]
    public float m_aoeRange;

    [Tooltip("The amount of force with which the player is launched when attacked by this enemy.")]
    public float m_attackLaunchForce;

    #endregion


    public override IEnumerator AttackTarget( )
    {

        yield return new WaitForSeconds( m_windupDuration );

        Collider2D playerHit = Physics2D.OverlapCircle( transform.position , m_aoeRange , m_playerLayer );

        if( playerHit != null )
        {
            PlayerController player = playerHit.gameObject.GetComponent<PlayerController>();

            player.m_knockbackForce = m_attackLaunchForce;

            player.m_knockbackDirection = player.gameObject.transform.position - transform.position;

            player.GetComponent<HealthManager>( ).TakeDamage( m_attackDamage );

            StartCoroutine( player.KnockBack( ) );

        }

        yield return new WaitForSeconds( m_attackInterval );

        m_isAttacking = false;

    }

}
