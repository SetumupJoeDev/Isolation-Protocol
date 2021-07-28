using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{

    public List<TutorialTask> m_tutorialTasks;

    public TutorialTask m_currentTask;

    public int m_currentTaskIndex = 0;

    public TextMeshProUGUI m_taskName;
    public TextMeshProUGUI m_taskDescription;
    public TextMeshProUGUI m_taskVariableName;
    public TextMeshProUGUI m_taskCounter;

    public PlayerController m_playerController;

    private void Start( )
    {

        m_tutorialTasks.Add( new MovementTask() );

        m_tutorialTasks.Add( new DodgeTask( ) );

        foreach(TutorialTask task in m_tutorialTasks )
        {
            task.Initialise( );
        }
        m_currentTask = m_tutorialTasks[m_currentTaskIndex];
        StartNewTask( );
    }

    public void StartNewTask( )
    {

        m_taskName.text = m_currentTask.m_taskTitle;

        m_taskDescription.text = m_currentTask.m_taskDescription;

        m_taskVariableName.text = m_currentTask.m_taskVariableName;

        m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString();

    }

    public void Update( )
    {

        m_currentTask.m_taskGoal.CheckObjectiveProgress( );

        m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString( );

        if ( m_currentTask.m_taskGoal.GoalAchieved( ) )
        {
            m_currentTaskIndex++;
            m_currentTask = m_tutorialTasks[m_currentTaskIndex];
            StartNewTask( );
        }

    }

}
