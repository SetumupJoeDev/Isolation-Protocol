using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamEnemy : EnemyBase
{
    public AudioClip m_windupSound; // Lewis' code
    public AudioClip m_attackSound;// Lewis' code
  
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

    


        gameObject.transform.localScale = new Vector3(1.5f, 1.5f);// Lewis' code, Makes enemy larger to indicate windup
        AudioSource.PlayClipAtPoint(m_windupSound, transform.position); // Lewis' code, play sound to indicate windup
        yield return new WaitForSeconds( m_windupDuration );
        gameObject.transform.localScale = new Vector3(1f, 1f); // Reset's enemy size 
        Collider2D playerHit = Physics2D.OverlapCircle( transform.position , m_aoeRange , m_playerLayer );

        if( playerHit != null )
        {
            AudioSource.PlayClipAtPoint(m_attackSound, transform.position); // Lewis' code, play sound to indicate windup
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
