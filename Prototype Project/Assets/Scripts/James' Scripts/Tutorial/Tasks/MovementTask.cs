using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : TutorialTask
{
    
    public override void Initialise( )
    {

        m_taskGoal = new MovementGoal( );

        m_taskGoal.Initialise( );

    }

}
