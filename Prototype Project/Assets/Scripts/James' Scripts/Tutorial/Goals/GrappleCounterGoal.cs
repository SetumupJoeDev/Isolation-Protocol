using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCounterGoal : TaskGoal
{
    
    //The constructor for the GrappleCounterGoal class
    public GrappleCounterGoal( int goalAmount = 1 )
    {
        m_goalAmount = goalAmount;

        //Subscribes the HoloSlugTrigger event to the OnHoloSlugDeath event
        TutorialEventListener.current.OnHoloSlugDeath += HoloSlugTrigger;

    }

    public void HoloSlugTrigger( )
    {
        //Increments currentAmount by one
        m_currentAmount++;
    }

}
