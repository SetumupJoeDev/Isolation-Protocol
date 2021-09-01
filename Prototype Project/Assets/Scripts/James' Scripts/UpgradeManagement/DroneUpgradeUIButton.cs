using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUpgradeUIButton : InterfaceButton
{

    [Tooltip("The upgrade station that managed the drone upgrades.")]
    public DroneWorkbench m_droneWorkbench;

    public override void OnClick( )
    {
        //If the name of the product associated with this button is Drone, then the combat drone itself is toggled
        if(m_productName.text == "Drone" )
        {
            m_droneWorkbench.ToggleCombatDrone( );
        }
        //Otherwise, the upgrade associated with this button is activated
        else
        {
            m_droneWorkbench.ToggleUpgrade( m_buttonID - 1 );
        }
    }


}
