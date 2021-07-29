using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCounterGoal : TaskGoal
{
    
    public GrappleCounterGoal( int goalAmount = 1 )
    {
        m_goalAmount = goalAmount;

        TutorialEventListener.current.OnHoloSlugDeath += HoloSlugTrigger;

    }

    public void HoloSlugTrigger( )
    {
        m_currentAmount++;
    }

}
