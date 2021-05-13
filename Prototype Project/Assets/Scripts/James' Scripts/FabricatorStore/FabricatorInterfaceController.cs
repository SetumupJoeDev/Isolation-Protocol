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
        m_currentMenu = m_upgradeSelectionMenu;
    }

    public void ShowWeaponList( )
    {
        m_currentMenu.SetActive( false );

        m_weaponsList.SetActive( true );

        m_currentMenu = m_weaponsList;
    }

    public void ShowExoSuitUpgrades( )
    {
        m_currentMenu.SetActive( false );

        m_exoSuitUpgrades.SetActive( true );

        m_currentMenu = m_exoSuitUpgrades;
    }

    public void ShowDroneUpgrades( )
    {
        m_currentMenu.SetActive( false );

        m_droneUpgrades.SetActive( true );

        m_currentMenu = m_droneUpgrades;
    }

    public void ShowSelectionMenu( )
    {
        m_currentMenu.SetActive( false );

        m_upgradeSelectionMenu.SetActive( true );

        m_currentMenu = m_upgradeSelectionMenu;
    }

}
