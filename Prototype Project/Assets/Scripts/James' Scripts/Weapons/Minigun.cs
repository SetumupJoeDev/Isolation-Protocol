using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : ConventionalWeapon
{

    [Tooltip("The script used to control the player.")]
    public PlayerController m_playerController;

    [Tooltip("The speed debuff applied to the player when they equip the minigun.")]
    public float m_playerSpeedDebuff;

    public enum firingState { idle, windingUp, spinning, firing, stoppingFiring, windingDown };

    [Tooltip("The current firing state of the minigun.")]
    public firingState m_currentFiringState;

    [Header("Minigun Sounds")]

    [Tooltip("The sound that plays when the minigun is spinning up.")]
    public AudioClip m_minigunWindUp;

    [Tooltip("The sound that plays when the minigun is spinning but not firing.")]
    public AudioClip m_minigunSpinLoop;

    [Tooltip("The sound that plays when the minigun is starts firing.")]
    public AudioClip m_minigunStartFiring;

    [Tooltip("The sound that plays when the minigun is firing.")]
    public AudioClip m_minigunFireLoop;

    [Tooltip("The sound that plays when the minigun stops firing.")]
    public AudioClip m_minigunStopFiring;

    [Tooltip("The sound that plays when the minigun is spinning down.")]
    public AudioClip m_minigunWindDown;

    [Tooltip("A boolean that determines whether or not a behaviour is currently running.")]
    public bool m_behaviourActive;

    public void OnEnable( )
    {
        //Find and assigns the playercontroller
        m_playerController = GameObject.Find( "Player" ).GetComponent<PlayerController>();

        //Adds the speed debuff to the player's move speed
        m_playerController.m_moveSpeed += m_playerSpeedDebuff;

    }

    public override void FireWeapon( )
    {

        if ( !m_behaviourActive )
        {

            switch ( m_currentFiringState )
            {
                case ( firingState.idle ):
                    {
                        //Sets the firing sound clip as the minigun windup sound
                        m_fireSound.clip = m_minigunWindUp;

                        //Sets this to false so that the windup sound doesn't loop
                        m_fireSound.loop = false;

                        //Plays the weapon's audiosource
                        m_fireSound.Play( );

                        //Runs the current behaviour and passes in the next one
                        StartCoroutine( RunCurrentBehaviour( firingState.windingUp ) );

                    }
                    break;
                case ( firingState.windingUp ):
                    {
                        //If the weapon is firing and has enough ammo to fire again then the weapon fires
                        if ( m_isWeaponFiring && ( int )m_currentMagAmmo - m_projectilesPerShot >= 0 )
                        {
                            //Sets the weapon's current sound clip as the firing loop
                            m_fireSound.clip = m_minigunFireLoop;

                            //Sets this to true so that the firing sound will loop while the weapon fires
                            m_fireSound.loop = true;

                            //If the sound isn't currently playing, it plays. This is to prevent the sound from restarting on every shot
                            if ( !m_fireSound.isPlaying )
                            {
                                m_fireSound.Play( );
                            }

                            //Starts the firing coroutine
                            StartCoroutine( FireProjectiles( ) );
                        }
                        //Otherwise if the weapon doesn't have enough ammo left, the weapon simply spins
                        else if( m_isWeaponFiring )
                        {
                            //Sets the weapon's sound as the spin loop
                            m_fireSound.clip = m_minigunSpinLoop;

                            //Sets this to true so that the weapon sound will loop
                            m_fireSound.loop = true;

                            //Plays the weapon's firing sound
                            m_fireSound.Play( );

                            //Runs the current behaviour and passes in the next
                            StartCoroutine( RunCurrentBehaviour( firingState.spinning ) );
                        }

                    }
                    break;
            }
        }
    }

    public override void StopFiring( )
    {
        switch ( m_currentFiringState )
        {
            case ( firingState.firing ):
            {
                    //Sets the weapon's sound clip as the sound the minigun makes when it stops firing
                    m_fireSound.clip = m_minigunStopFiring;

                    //Sets this to false so that the sound doesn't loop
                    m_fireSound.loop = false;

                    //Plays the stop-firing sound
                    m_fireSound.Play( );

                    //Runs the current behaviour and passes in the next
                    StartCoroutine( RunCurrentBehaviour( firingState.idle ) );

            }
            break;

            case ( firingState.spinning ):
            case ( firingState.windingUp ):
                {
                    //Sets the weapon's sound as the winding down sound
                    m_fireSound.clip = m_minigunWindDown;

                    //Sets this to false so that the weapon sound doesn't loop
                    m_fireSound.loop = false;

                    //Plays the winding down sound
                    m_fireSound.Play( );

                    //Runs the current behaviour and passes in the next
                    StartCoroutine( RunCurrentBehaviour( firingState.idle ) );
                }
            break;
        }
    }

    public IEnumerator RunCurrentBehaviour( firingState nextState )
    {

        //Sets this to true so that only one behaviour can be run at once
        m_behaviourActive = true;

        //Sets the current firing state to the one passed in
        m_currentFiringState = nextState;

        //Waits for the duration of the current firing sound clip
        yield return new WaitForSeconds( m_fireSound.clip.length );

        //Sets this to false so that the next behaviour can play out correctly
        m_behaviourActive = false;

    }

    public void OnDisable( )
    {
        //Subtracts the speed debuff from the player's move speed
        m_playerController.m_moveSpeed -= m_playerSpeedDebuff;
    }

}
