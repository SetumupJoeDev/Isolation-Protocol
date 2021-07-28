using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialTask
{

    public string m_taskTitle;

    public string m_taskDescription;

    public string m_taskVariableName;

    public TaskGoal m_taskGoal;

    public virtual void Initialise( )
    {
        //Initialisation logic goes here
    }

}
