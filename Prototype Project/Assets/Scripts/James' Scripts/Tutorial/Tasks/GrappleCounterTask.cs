using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCounterTask : TutorialTask
{

    public override void Initialise()
    {

        m_taskGoal = new GrappleCounterGoal();

        m_taskTitle = "Grapple Counter";

        m_taskDescription = "Use your dodge ability to remove the grappling enemy.";

        m_taskVariableName = "Grapples countered:";

        m_taskGoal.Initialise( );

    }

    public override void StartTask()
    {
        TutorialEventListener.current.GrappleTaskStart( );
    }

}
