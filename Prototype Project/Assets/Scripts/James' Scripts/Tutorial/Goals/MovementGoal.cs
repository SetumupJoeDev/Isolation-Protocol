using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGoal : TaskGoal
{

    public bool m_movedLeft;

    public bool m_movedRight;

    public bool m_movedUp;

    public bool m_movedDown;

    public MovementGoal( int goalAmount = 4 )
    {
        m_goalAmount = goalAmount;
    }

    public override void CheckObjectiveProgress( )
    {
        if ( !m_movedUp && Input.GetKeyDown( KeyCode.W ) )
        {
            m_movedUp = true;
            m_currentAmount++;
        }
        if ( !m_movedDown && Input.GetKeyDown( KeyCode.S ) )
        {
            m_movedDown = true;
            m_currentAmount++;
        }
        if ( !m_movedLeft && Input.GetKeyDown( KeyCode.A ) )
        {
            m_movedLeft = true;
            m_currentAmount++;
        }
        if ( !m_movedRight && Input.GetKeyDown( KeyCode.D ) )
        {
            m_movedRight = true;
            m_currentAmount++;
        }
        
    }

}
