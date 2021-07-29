using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGoal : TaskGoal
{

    public AimGoal ( int goalAmount = 4 )
    {
        m_goalAmount = goalAmount;
    }

    public override void Initialise()
    {
        TutorialEventListener.current.OnDummyHit += DummyTrigger;
    }

    public void DummyTrigger()
    {
        m_currentAmount++;
        if ( GoalAchieved( ) )
        {
            TutorialEventListener.current.AimTaskEnd( );
        }
    }

}
