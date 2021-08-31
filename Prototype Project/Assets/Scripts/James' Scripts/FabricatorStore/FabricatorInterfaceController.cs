using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorInterfaceController : MonoBehaviour
{

    [Tooltip("The GameObject that contains the initial upgrade selection choices.")]
    public GameObject m_upgradeSelectionMenu;
    
    [Tooltip("The GameObject that contains the weapon purchase screen.")]
    public GameObject m_weaponsList;
    
    [Tooltip("The GameObject that contains the Player Upgrade purchase screen.")]
    public GameObject m_exoSuitUpgrades;
     
    [Tooltip("The GameObject that contains the Drone Upgrade purchase screen.")]
    public GameObject m_droneUpgrades;
     
    [Tooltip("The currently active menu of the interface.")]
    public GameObject m_currentMenu;

    public void Start( )
    {
        //Sets the initial menu the UI opens to be the selection menu so that the player can choose what type of upgrade they want to buy
        m_currentMenu = m_upgradeSelectionMenu;
    }

    public void ShowWeaponList( )
    {
        //Disables the current menu to hide it from the player
        m_currentMenu.SetActive( false );

        //Enables the weapons list menu so the player can interact with it
        m_weaponsList.SetActive( true );

        //Sets the current menu to be the weapons list
        m_currentMenu = m_weaponsList;
    }

    public void ShowExoSuitUpgrades( )
    {
        //Disables the current menu to hide it from the player
        m_currentMenu.SetActive( false );

        //Enables the exosuit upgrade list menu so the player can interact with it
        m_exoSuitUpgrades.SetActive( true );

        //Sets the current menu to be the exosuit upgrade list
        m_currentMenu = m_exoSuitUpgrades;
    }

    public void ShowDroneUpgrades( )
    {
        //Disables the current menu to hide it from the player
        m_currentMenu.SetActive( false );

        //Enables the drone upgrade list menu so the player can interact with it
        m_droneUpgrades.SetActive( true );

        //Sets the current menu to be the drone upgrade list
        m_currentMenu = m_droneUpgrades;
    }

    public void ShowSelectionMenu( )
    {
        //Disables the current menu to hide it from the player
        m_currentMenu.SetActive( false );

        //Enables the upgrade selection menu so the player can interact with it
        m_upgradeSelectionMenu.SetActive( true );

        //Sets the current menu to be the upgrade selection
        m_currentMenu = m_upgradeSelectionMenu;
    }

}
