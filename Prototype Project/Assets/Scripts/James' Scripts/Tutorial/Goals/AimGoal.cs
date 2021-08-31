using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGoal : TaskGoal
{
    //The constructor for the AimGoal class
    public AimGoal ( int goalAmount = 4 )
    {
        m_goalAmount = goalAmount;
    }

    public override void Initialise()
    {
        //Subscribes the DummyTrigger method to the OnDummyHit event
        TutorialEventListener.current.OnDummyHit += DummyTrigger;
    }

    public void DummyTrigger()
    {
        //Increments the value of currentAmount by one
        m_currentAmount++;

        //If the goal has been achieved, the AimTaskEnd event is called
        if ( GoalAchieved( ) )
        {
            TutorialEventListener.current.AimTaskEnd( );
        }
    }

}
