using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUpgradeUIButton : InterfaceButton
{

    public DroneWorkbench m_droneWorkbench;

    public override void OnClick( )
    {
        if(m_productName.text == "Drone" )
        {
            m_droneWorkbench.ToggleCombatDrone( );
        }
        else
        {
            m_droneWorkbench.ToggleUpgrade( m_buttonID - 1 );
        }
    }


}
