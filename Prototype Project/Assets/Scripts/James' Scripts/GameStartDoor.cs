﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{
    [Tooltip("Toggles hidden player objects to show in level scene")]
    [SerializeField]
    private GameObject[] m_togglePlayerObjects;

    public LoadingScreenController m_loadingScreenController;

    public override void Activated( PlayerController playerController )
    {

        StartCoroutine( m_loadingScreenController.FadeIn( this ) );

    }

    public void LoadLevel( )
    {
        // Loads the 1st level
        SceneManager.LoadScene( "Level 1" );

        m_playerController.ToggleWeaponsFree( );

        m_playerController.m_currentWeapon.SetActive( true );

        for ( int i = 0; i < m_togglePlayerObjects.Length; i++ )
        {
            m_togglePlayerObjects[i].SetActive( true );
        }
    }

}
