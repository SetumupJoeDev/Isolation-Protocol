using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTask : TutorialTask
{

    public override void Initialise()
    {

        m_taskGoal = new AimGoal();

        m_taskTitle = "Aiming & Shooting";

        m_taskDescription = "Use the left mouse button to fire, aim towards the dummies using the cursor.";

        m_taskVariableName = "Dummies hit:";

        m_taskGoal.Initialise();

    }

    public override void StartTask()
    {
        TutorialEventListener.current.AimTaskStart( );
    }

}
