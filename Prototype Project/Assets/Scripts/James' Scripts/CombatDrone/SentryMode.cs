using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SentryMode : ActiveDroneBehaviourBase
{

    #region Behaviour Parameters

    #region Combat

    [Header("Combat")]

    [Tooltip("A boolean that determines whether or not the drone is currently firing.")]
    public bool m_isFiring;

    [Tooltip("The radius in which targets must be for the drone to fire on them.")]
    public float m_fireRadius;

    [Tooltip("The time before each shot fired by the drone.")]
    public float m_fireInterval;

    [Tooltip("The prefab for the projectile the drone will fire.")]
    public GameObject m_projectilePrefab;

    [Tooltip("The layer upon which the drone's targets will sit.")]
    public LayerMask m_targetLayer;

    //The enemy that the drone is currently targeting
    private GameObject m_currentEnemy;

    //The direction in which the drone is currently aiming, usually at its current enemy
    private Vector3 m_aimingDirection;

    [Tooltip("The amount of time for which the drone remains in sentry mode.")]
    public float m_sentryModeDuration;

    #endregion

    #endregion

    #region Sounds

    [Header("Sounds")]

    [Tooltip("The sound that plays when the drone enters sentry mode.")]
    public AudioClip m_setupSound;

    [Tooltip("The sound that plays when the drone leaves sentry mode.")]
    public AudioClip m_deactivatedSound;

    [Tooltip("The sound that plays when the drone fires.")]
    public AudioClip m_firingSound;

    #endregion

    public override void Update( )
    {
        if ( Input.GetKeyDown( KeyCode.Q ) )
        {
            //If the player presses Q and the behaviour is not currently active or in cooldown then the behaviour is activated
            if ( !m_behaviourActive && !m_isInCooldown )
            {
                ActivateEffect( );
            }
            //If the behaviour is active, then it is deactivated and the cooldown timer is activated
            else if( m_behaviourActive )
            {

                DisableModuleBehaviour( );

                StartCoroutine( CooldownTimer( ) );

            }
        }
        //If the drone is not currently firing, the behaviour is active, it doesn't currently have a target but there are targets in range, then it fires on an enemy
        if ( !m_isFiring && m_behaviourActive && m_currentEnemy == null && CheckForTargets( ) )
        {
            StartCoroutine( FireAtEnemy( ) );
        }
    }

    public IEnumerator FireAtEnemy( )
    {

        m_isFiring = true;

        //Calculates an aiming direction based on the position of the enemy and the drone
        m_aimingDirection = m_currentEnemy.transform.position - transform.position;

        //Instantiates a new projectile object and gives it the directional velocity calculated above
        ProjectileBase newProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBase>( );

        newProjectile.m_projectileVelocity = m_aimingDirection;

        //Plays the drone's firing sound
        AudioSource.PlayClipAtPoint( m_firingSound , transform.position );

        //Waits for the length of the fireInterval before attempting to fire again
        yield return new WaitForSeconds( m_fireInterval );

        //If the currentEnemy is null, it has been killed and the drone stops firing
        if ( m_currentEnemy == null )
        {
            m_isFiring = false;
        }
        //Otherwise, the enemy is still alive and the drone continues firing on it
        else
        {
            StartCoroutine( FireAtEnemy( ) );
        }

    }

    public bool CheckForTargets( )
    {
        //Uses an overlap circle to check for any targets within a radius
        Collider2D newTarget = Physics2D.OverlapCircle(transform.position, m_fireRadius, m_targetLayer);

        //If a target is found, it is set as the currentEnemy and this method returns true
        if ( newTarget )
        {
            m_currentEnemy = newTarget.gameObject;
            return true;
        }
        //Otherwise, the method returns false
        else
        {
            return false;
        }

    }

    public override void EnableModuleBehaviour( )
    {
        //Plays the setup sound to indicate that the drone has entered sentry mode
        AudioSource.PlayClipAtPoint( m_setupSound , transform.position );

        //Sets bahaviourActive to true and starts the SentryTimer coroutine
        m_behaviourActive = true;

        StartCoroutine( SentryTimer( ) );

    }

    public IEnumerator SentryTimer( )
    {
        //Waits for the duration of sentryModeDuration before disabling the behaviour and starting the cooldown coroutine
        yield return new WaitForSeconds( m_sentryModeDuration );

        DisableModuleBehaviour( );

        StartCoroutine( CooldownTimer( ) );

    }

    public override void DisableModuleBehaviour( )
    {
        //Plays the deactivation sound to signal that the drone has left sentry mode
        AudioSource.PlayClipAtPoint( m_deactivatedSound , transform.position );

        //Sets isFiring to false so that the drone can fire again next time the behaviour is used
        m_isFiring = false;

        //Sets this to false so that the behaviour can be reactivated later
        m_behaviourActive = false;

        //Re-enables the drone's basic behaviours so it will follow the player and shoot at enemies occasionally
        m_droneController.EnableBasicBehaviours( );
        
        //Stops any coroutines from running to prevent the drone from continuing to fire
        StopAllCoroutines( );

    }

}
