using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DroneWorkbench : UpgradeManager
{

    [Tooltip("The Combat Drone associated with the player.")]
    public DroneController m_drone;

    public void ToggleCombatDrone( )
    {
        //Toggles the drone on and off when the button is pressed
        m_drone.gameObject.SetActive( !m_drone.gameObject.activeSelf );

        //Calls the quicksave event to save the player's active upgrades
        SaveSystem.SavePlayer(m_playerController);
    }

    public void ToggleUpgrade( int upgradeIndex )
    {
        //Enables the upgrade at the index passed in by the button
        m_drone.EnableUpgrade( upgradeIndex );

        //Calls the quicksave event to save the player's active upgrades
        SaveSystem.SavePlayer(m_playerController);
    }

}
