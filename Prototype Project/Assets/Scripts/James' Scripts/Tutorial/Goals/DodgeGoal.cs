using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGoal : TaskGoal
{

    public DodgeGoal(int requiredAmount = 2 )
    {
        m_goalAmount = requiredAmount;
    }

    public override void CheckObjectiveProgress( )
    {
        if ( Input.GetMouseButtonDown( 1 ) )
        {
            m_currentAmount++;
        }
    }

}
