using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{
    [Tooltip("Toggles hidden player objects to show in level scene")]
    [SerializeField]
    private GameObject[] m_togglePlayerObjects;

    public LevelLoader m_levelLoader;

    private string m_levelToLoad = "Level 1";

    public LoadingScreenController m_loadingScreenController;

    public override void Activated( PlayerController playerController )
    {

        gameEvents.hello.loadingLevel( m_levelLoader, m_levelToLoad );

        EnablePlayerCombatObjects( );

    }

    public void EnablePlayerCombatObjects( )
    {
        m_playerController.ToggleWeaponsFree( );

        m_playerController.m_currentWeapon.SetActive( true );

        for ( int i = 0; i < m_togglePlayerObjects.Length; i++ )
        {
            m_togglePlayerObjects[i].SetActive( true );
        }
    }

}
