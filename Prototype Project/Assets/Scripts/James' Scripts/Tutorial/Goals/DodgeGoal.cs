using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGoal : TaskGoal
{
    //The constructor for the DodgeGoal class
    public DodgeGoal(int requiredAmount = 2 )
    {
        m_goalAmount = requiredAmount;
    }

    public override void CheckObjectiveProgress( )
    {
        //If the right mouse button is clicked, currentAmount is incremented
        if ( Input.GetMouseButtonDown( 1 ) )
        {
            m_currentAmount++;
        }
    }

}
