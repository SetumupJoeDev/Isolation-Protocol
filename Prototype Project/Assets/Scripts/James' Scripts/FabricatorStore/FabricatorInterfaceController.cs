using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorInterfaceController : MonoBehaviour
{

    [SerializeField]
    private GameObject m_upgradeSelectionMenu;

    [SerializeField]
    private GameObject m_weaponsList;

    [SerializeField]
    private GameObject m_exoSuitUpgrades;

    [SerializeField]
    private GameObject m_droneUpgrades;

    private GameObject m_currentMenu;

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
