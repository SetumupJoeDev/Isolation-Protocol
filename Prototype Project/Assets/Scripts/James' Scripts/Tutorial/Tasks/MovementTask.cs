using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : TutorialTask
{
    

    public override void Initialise( )
    {
        //Initialises the task by creating a new goal and assigning the task's information

        m_taskGoal = new MovementGoal( );

        m_taskTitle = "Movement";

        m_taskDescription = "Use the WASD keys to move in each of the four directions.";

        m_taskVariableName = "Keys pressed:";

        m_taskGoal.Initialise( );

    }

}
