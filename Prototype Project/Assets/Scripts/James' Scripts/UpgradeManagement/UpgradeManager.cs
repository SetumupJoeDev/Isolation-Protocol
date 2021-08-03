﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : InteractableObject
{

    public CanvasGroup m_upgradeUI;

    public GameObject m_noUpgradesText;

    public FabricatorStoreProduct[] m_availableUpgrades;

    public ButtonListGenerator m_buttonGenerator;

    public GameObject[] m_upgradeButtons;

    public override void Activated( PlayerController playerController )
    {

        base.Activated( playerController );

        m_playerController.m_isInMenu = !m_playerController.m_isInMenu;

        CheckForNewUnlocks( );

        ToggleUpgradeCanvas( );

    }

    public void CheckForNewUnlocks( )
    {

        bool anyUpgradesUnlocked = false;

        for ( int i = 0; i < m_buttonGenerator.m_buttonObjectReferences.Length; i++ )
        {
            if ( m_buttonGenerator.m_buttonObjectReferences[i].GetIsUnlocked( ) )
            {

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
