using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCounterTask : TutorialTask
{

    public override void Initialise()
    {
        //Initialises the task by creating a new goal and assigning the task's information

        m_taskGoal = new GrappleCounterGoal();

        m_taskTitle = "Grapple Counter";

        m_taskDescription = "Use your dodge ability to remove the grappling enemy.";

        m_taskVariableName = "Grapples countered:";

        m_taskGoal.Initialise( );

    }

    public override void StartTask()
    {
        //Calls the GrappleTaskStart event
        TutorialEventListener.current.GrappleTaskStart( );
    }

}
