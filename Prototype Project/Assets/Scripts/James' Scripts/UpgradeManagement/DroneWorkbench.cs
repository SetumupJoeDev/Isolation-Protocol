using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DroneWorkbench : UpgradeManager
{

    public DroneController m_drone;

    public void ToggleCombatDrone( )
    {
        //Toggles the drone on and off when the button is pressed
        m_drone.gameObject.SetActive( !m_drone.gameObject.activeSelf );
    }

    public void ToggleUpgrade( int upgradeIndex )
    {
        m_drone.EnableUpgrade( upgradeIndex );
    }

}
