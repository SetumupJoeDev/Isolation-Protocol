using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{

    [Tooltip("Toggles hidden player objects to show in level scene.")]
    public GameObject[] m_togglePlayerObjects;

    [Tooltip("The level loading script attached to this object.")]
    public LevelLoader m_levelLoader;

    //The name of the scene that the start door should load
    private string m_levelToLoad = "Level 1";

    [Tooltip("The loading screen controller attached to the game's loading screen.")]
    public LoadingScreenController m_loadingScreenController;

    public override void Activated( PlayerController playerController )
    {
        //Calls the loading level event on the game events manager
        gameEvents.hello.loadingLevel( m_levelLoader, m_levelToLoad );

        //Enables all of the objects the player needs for combat that were disabled in the hub
        EnablePlayerCombatObjects( );

    }

    public void EnablePlayerCombatObjects( )
    {

        //Sets the player as weapons free, so they can use their weapons
        m_playerController.ToggleWeaponsFree( );

        //Activates the player's current weapon
        m_playerController.m_currentWeapon.SetActive( true );

        //Loops through each of the player's objects and enables them
        for ( int i = 0; i < m_togglePlayerObjects.Length; i++ )
        {
            m_togglePlayerObjects[i].SetActive( true );
        }
    }

}
