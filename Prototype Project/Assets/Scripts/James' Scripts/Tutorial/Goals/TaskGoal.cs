using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskGoal
{

    //The goal amount that the player must reach to complete the quest
    public int m_goalAmount;

    //The current amount that the player has reached
    public int m_currentAmount;

    public virtual void Initialise( )
    {
        CheckObjectiveProgress( );
    }

    public bool GoalAchieved( )
    {
        //Returns true or false, depending on the outcome of the comparison
        return ( m_currentAmount >= m_goalAmount );
    }

    public virtual void CheckObjectiveProgress( )
    {
        //Objective logic goes here
    }

}
