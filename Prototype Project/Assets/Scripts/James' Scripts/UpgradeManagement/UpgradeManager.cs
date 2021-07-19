using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : InteractableObject
{

    public CanvasGroup m_upgradeUI;

    public GameObject m_noUpgradesText;

    public FabricatorStoreProduct[] m_availableUpgrades;

    public GameObject[] m_upgradeButtons;

    public override void Activated( )
    {

        m_playerController.m_isInMenu = !m_playerController.m_isInMenu;

        CheckForNewUnlocks( );

        ToggleUpgradeCanvas( );

    }

    public void CheckForNewUnlocks( )
    {

        bool anyUpgradesUnlocked = false;

        for ( int i = 0; i < m_availableUpgrades.Length; i++ )
        {
            if ( m_availableUpgrades[i].GetIsUnlocked( ) )
            {
                m_upgradeButtons[i].SetActive( true );

                anyUpgradesUnlocked = true;

            }
        }

        if ( !anyUpgradesUnlocked )
        {
            m_noUpgradesText.SetActive( true );
        }
        else if ( m_noUpgradesText.activeSelf )
        {
            m_noUpgradesText.SetActive( false );
        }

    }

    public void ToggleUpgradeCanvas( )
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

}
