using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskGoal
{

    public int m_goalAmount;

    public int m_currentAmount;

    public virtual void Initialise( )
    {
        CheckObjectiveProgress( );
    }

    public bool GoalAchieved( )
    {
        return ( m_currentAmount >= m_goalAmount );
    }

    public virtual void CheckObjectiveProgress( )
    {
        //Objective logic goes here
    }

}
