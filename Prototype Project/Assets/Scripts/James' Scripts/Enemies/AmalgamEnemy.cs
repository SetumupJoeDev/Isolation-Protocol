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

    [SerializeField]
    private float m_knockbackTime;

    private Shockwave m_shockwave;

    #endregion

    private void Start( )
    {
        m_shockwave = GetComponent<Shockwave>( );
    }

    public override IEnumerator AttackTarget( )
    {

        gameObject.transform.localScale = new Vector3(1.5f, 1.5f);// Lewis' code, Makes enemy larger to indicate windup

        AudioSource.PlayClipAtPoint(m_windupSound, transform.position); // Lewis' code, play sound to indicate windup

        //Waits for the duration of the windup before attacking to give the player the opportunity to avoid the attack
        yield return new WaitForSeconds( m_windupDuration );

        gameObject.transform.localScale = new Vector3(1f, 1f); // Reset's enemy size 

        m_shockwave.enabled = true;

        //Waits for the duration of the attack interval before having the opportunity to attack again
        yield return new WaitForSeconds( m_attackInterval );

        m_isAttacking = false;

    }

}
