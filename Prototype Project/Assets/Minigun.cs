using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : ConventionalWeapon
{

    public PlayerController m_playerController;

    public float m_playerSpeedDebuff;

    public enum firingState { idle, windingUp, spinning, firing, stoppingFiring, windingDown };

    public firingState m_currentFiringState;

    [Header("Minigun Sounds")]

    public AudioClip m_minigunWindUp;

    public AudioClip m_minigunSpinLoop;

    public AudioClip m_minigunStartFiring;

    public AudioClip m_minigunFireLoop;

    public AudioClip m_minigunStopFiring;

    public AudioClip m_minigunWindDown;

    public bool m_behaviourActive;

    public void OnEnable( )
    {
        m_playerController = GameObject.Find( "Player" ).GetComponent<PlayerController>();

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
                        m_fireSound.clip = m_minigunWindUp;

                        m_fireSound.loop = false;

                        m_fireSound.Play( );

                        StartCoroutine( RunCurrentBehaviour( firingState.windingUp ) );

                    }
                    break;
                case ( firingState.windingUp ):
                    {

                        if ( m_canWeaponFire && ( int )m_currentMagAmmo - m_projectilesPerShot >= 0 )
                        {

                            m_fireSound.clip = m_minigunFireLoop;

                            m_fireSound.loop = true;

                            if ( !m_fireSound.isPlaying )
                            {
                                m_fireSound.Play( );
                            }

                            StartCoroutine( FireProjectiles( ) );
                        }
                        else if( m_canWeaponFire )
                        {

                            m_fireSound.clip = m_minigunSpinLoop;

                            m_fireSound.loop = true;

                            m_fireSound.Play( );

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

                    m_fireSound.clip = m_minigunStopFiring;

                    m_fireSound.loop = false;

                    m_fireSound.Play( );

                    StartCoroutine( RunCurrentBehaviour( firingState.idle ) );

            }
            break;

            case ( firingState.spinning ):
            case ( firingState.windingUp ):
                {
                    m_fireSound.clip = m_minigunWindDown;

                    m_fireSound.loop = false;

                    m_fireSound.Play( );

                    StartCoroutine( RunCurrentBehaviour( firingState.idle ) );
                }
            break;
        }
    }

    public IEnumerator RunCurrentBehaviour( firingState nextState )
    {

        m_behaviourActive = true;

        m_currentFiringState = nextState;

        yield return new WaitForSeconds( m_fireSound.clip.length );

        m_behaviourActive = false;

    }

    public void OnDisable( )
    {
        m_playerController.m_moveSpeed -= m_playerSpeedDebuff;
    }

}
