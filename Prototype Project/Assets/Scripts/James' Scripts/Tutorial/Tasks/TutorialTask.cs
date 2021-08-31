using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialTask
{

    //The title of the task
    public string m_taskTitle;

    //The description of the task
    public string m_taskDescription;

    //The name of the variable the task tracks, e.g enemies killed, shots fired, levels completed etc.
    public string m_taskVariableName;

    //The goal of this task, a class that tracks the progress in the task
    public TaskGoal m_taskGoal;

    public virtual void Initialise( )
    {
        //Initialisation logic goes here
    }

    public virtual void StartTask()
    {
        //Task starting goes here
    }

}
