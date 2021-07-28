using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTask : TutorialTask
{
    public override void Initialise( )
    {

        m_taskGoal = new DodgeGoal( );

        m_taskTitle = "Dodging";

        m_taskDescription = "Use the right mouse button to dodge towards the mouse cursor.";

        m_taskVariableName = "Times dodged:";

        m_taskGoal.Initialise( );

    }
}
