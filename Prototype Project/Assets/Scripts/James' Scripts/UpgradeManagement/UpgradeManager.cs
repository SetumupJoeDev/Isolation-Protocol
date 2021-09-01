using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : InteractableObject
{

    [Tooltip("The UI canvas that holds the upgrade management UI.")]
    public CanvasGroup m_upgradeUI;

    [Tooltip("The UI Text element that is displayed when the player has no available upgrades.")]
    public GameObject m_noUpgradesText;

    [Tooltip("An array of available upgrades associated with this upgrade station.")]
    public FabricatorStoreProduct[] m_availableUpgrades;

    [Tooltip("The script that generates the list of buttons used to toggle upgrades.")]
    public ButtonListGenerator m_buttonGenerator;

    [Tooltip("The sound that plays when thye upgrade UI window is opened.")]
    public AudioSource m_windowOpenSound;

    [Tooltip("The sound that plays when thye upgrade UI window is closed.")]
    public AudioSource m_windowCloseSound;

    public override void Activated( PlayerController playerController )
    {

        base.Activated( playerController );

        //Sets the player as in a menu so they cannot move around
        m_playerController.m_isInMenu = !m_playerController.m_isInMenu;

        //Toggles the player's HUD off so that it doesn't obscure the menu
        m_playerController.ToggleHUD( );

        //Checks for any newly unlocked upgrades to add them to the list
        CheckForNewUnlocks( );

        //Enables the canvas to display the upgrade management UI
        ToggleUpgradeCanvas( );

        //Plays the window opening sound
        m_windowOpenSound.Play( );

    }

    public void PlayWindowCloseSound( )
    {
        //Plays the window closing sound
        m_windowCloseSound.Play( );
    }

    public void CheckForNewUnlocks( )
    {

        bool anyUpgradesUnlocked = false;

        //Loops through the array of upgrade scriptable objects to find any unlocked upgrades
        for ( int i = 0; i < m_buttonGenerator.m_buttonObjectReferences.Length; i++ )
        {
            if ( m_buttonGenerator.m_buttonObjectReferences[i].GetIsUnlocked( ) )
            {

                anyUpgradesUnlocked = true;

            }
        }

        //If no upgrades have been unlocked, then the noUpgradesText is enabled
        if ( !anyUpgradesUnlocked )
        {
            m_noUpgradesText.SetActive( true );
        }
        //Otherwise, if the text is enabled, it is disabled
        else if ( m_noUpgradesText.activeSelf )
        {
            m_noUpgradesText.SetActive( false );
        }
        //The list is cleared of all its current upgrades and repopulated to include the new ones
        else
        {
            m_buttonGenerator.ClearList();
            m_buttonGenerator.PopulateButtonList();
        }

    }

    public void ToggleUpgradeCanvas( )
    {
        //Toggles the canvas off if it is currently opaque
        if ( m_upgradeUI.alpha == 1 )
        {

            m_upgradeUI.alpha = 0;

            m_upgradeUI.blocksRaycasts = false;

            m_isActivated = false;

        }
        //Toggles the canvas on if it is currently transparent
        else
        {

            m_upgradeUI.alpha = 1;

            m_upgradeUI.blocksRaycasts = true;

        }
    }

}
