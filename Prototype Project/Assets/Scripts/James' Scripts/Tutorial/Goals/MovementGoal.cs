using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGoal : TaskGoal
{

    //A boolean that determines whether or not the player has moved left
    public bool m_movedLeft;

    //A boolean that determines whether or not the player has moved right
    public bool m_movedRight;

    //A boolean that determines whether or not the player has moved up
    public bool m_movedUp;

    //A boolean that determines whether or not the player has moved down
    public bool m_movedDown;

    public MovementGoal( int goalAmount = 4 )
    {
        m_goalAmount = goalAmount;
    }

    public override void CheckObjectiveProgress( )
    {
        //If the player has yet to move up and the W key is pressed, moveUp is set to true and currentAmount is incremented
        if ( !m_movedUp && Input.GetKeyDown( KeyCode.W ) )
        {
            m_movedUp = true;
            m_currentAmount++;
        }
        //If the player has yet to move down and the S key is pressed, moveUp is set to true and currentAmount is incremented
        if ( !m_movedDown && Input.GetKeyDown( KeyCode.S ) )
        {
            m_movedDown = true;
            m_currentAmount++;
        }
        //If the player has yet to move left and the A key is pressed, moveUp is set to true and currentAmount is incremented
        if ( !m_movedLeft && Input.GetKeyDown( KeyCode.A ) )
        {
            m_movedLeft = true;
            m_currentAmount++;
        }
        //If the player has yet to move right and the D key is pressed, moveUp is set to true and currentAmount is incremented
        if ( !m_movedRight && Input.GetKeyDown( KeyCode.D ) )
        {
            m_movedRight = true;
            m_currentAmount++;
        }
        
    }

}
