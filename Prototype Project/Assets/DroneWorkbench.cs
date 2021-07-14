using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DroneWorkbench : InteractableObject
{

    public DroneController m_drone;

    public CanvasGroup m_upgradeUI;

    public FabricatorStoreProduct[] m_droneUpgrades;

    public GameObject[] m_upgradeToggleButtons;

    public GameObject m_noUpgradesText;

    public override void Activated( )
    {

        m_playerController.m_isInMenu = !m_playerController.m_isInMenu;

        CheckForNewUnlocks( );

        ToggleUpgradeCanvas( );

    }

    public void ToggleCombatDrone( )
    {
        //Toggles the drone on and off when the button is pressed
        m_drone.gameObject.SetActive( !m_drone.gameObject.activeSelf );
    }

    public void CheckForNewUnlocks( )
    {

        bool anyUpgradesUnlocked = false;

        for( int i = 0; i < m_droneUpgrades.Length; i++ )
        {
            if( m_droneUpgrades[i].GetIsUnlocked( ) )
            {
                m_upgradeToggleButtons[i].SetActive( true );

                anyUpgradesUnlocked = true;

            }
        }

        if( !anyUpgradesUnlocked )
        {
            m_noUpgradesText.SetActive( true );
        }
        else if( m_noUpgradesText.activeSelf )
        {
            m_noUpgradesText.SetActive( false );
        }

    }

    public void ToggleUpgradeCanvas(  )
    {
        //Toggles the canvas off if it is currently opaque
        if ( m_upgradeUI.alpha == 1 )
        {

            m_upgradeUI.alpha = 0;

            m_upgradeUI.blocksRaycasts = false;

        }
        //Toggles the canvas on if it is currently transparent
        else
        {

            m_upgradeUI.alpha = 1;

            m_upgradeUI.blocksRaycasts = true;

        }
    }

    public void ToggleUpgrade( int upgradeIndex )
    {
        m_drone.EnableUpgrade( upgradeIndex );
    }

}
