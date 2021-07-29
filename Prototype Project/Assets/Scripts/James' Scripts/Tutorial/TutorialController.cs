using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{

    [Header("Booleans")]

    public bool m_isTaskActive;

    public bool m_tutorialComplete;

    [Header("Tasks")]

    public List<TutorialTask> m_tutorialTasks;

    public TutorialTask m_currentTask;

    [Header("Audio")]

    public AudioClip[] m_voiceClips;

    public AudioSource m_voiceLinePlayer;

    [Header("Dummy Enemies")]

    public GameObject m_dummies;

    public GameObject m_holoSlug;

    [Header("Indices")]

    public int m_currentTaskIndex = 0;

    public int m_voiceLineIndex = 0;

    [Header("User Interface")]

    public CanvasController m_menuCanvasController;

    public TextMeshProUGUI m_taskName;
    public TextMeshProUGUI m_taskDescription;
    public TextMeshProUGUI m_taskVariableName;
    public TextMeshProUGUI m_taskCounter;

    [Header("Player")]

    public PlayerController m_playerController;

    private void Start( )
    {

        TutorialEventListener.current.OnAimTaskStart += ToggleTrainingDummies;

        TutorialEventListener.current.OnAimTaskEnd += ToggleTrainingDummies;

        TutorialEventListener.current.OnGrappleTaskStart += ToggleHoloSlug;

        m_tutorialTasks.Add( new MovementTask( ) );

        m_tutorialTasks.Add( new DodgeTask( ) );

        m_tutorialTasks.Add( new AimTask( ) );

        m_tutorialTasks.Add( new GrappleCounterTask( ) );

        foreach(TutorialTask task in m_tutorialTasks )
        {
            task.Initialise( );
        }

        m_currentTask = m_tutorialTasks[ m_currentTaskIndex ];

        m_voiceLinePlayer.clip = m_voiceClips[ m_voiceLineIndex ];

        StartCoroutine( WaitForVoiceClip( ) );
    }

    public void StartNewTask( )
    {

        m_taskName.text = m_currentTask.m_taskTitle;

        m_taskDescription.text = m_currentTask.m_taskDescription;

        m_taskVariableName.text = m_currentTask.m_taskVariableName;

        m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString();

        m_currentTask.StartTask( );

    }

    public void Update( )
    {


        if ( !m_tutorialComplete )
        {

            if ( m_isTaskActive )
            {
                m_currentTask.m_taskGoal.CheckObjectiveProgress( );

                m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString( );
            }

            if ( m_currentTask.m_taskGoal.GoalAchieved( ) )
            {
                m_currentTaskIndex++;

                if ( m_currentTaskIndex >= m_tutorialTasks.Count )
                {
                    m_tutorialComplete = true;
                }
                else
                {
                    m_currentTask = m_tutorialTasks[m_currentTaskIndex];
                }

                StartCoroutine( WaitForVoiceClip( ) );
            }
        }

    }

    public IEnumerator WaitForVoiceClip( )
    {

        m_isTaskActive = false;

        m_voiceLinePlayer.Play( );

        m_voiceLineIndex++;

        yield return new WaitForSeconds( m_voiceLinePlayer.clip.length );

        if ( !m_tutorialComplete )
        {
            m_voiceLinePlayer.clip = m_voiceClips[m_voiceLineIndex];

            StartNewTask( );

            m_isTaskActive = true;
        }
        else
        {
            m_menuCanvasController.ToggleCanvas( );
        }

    }

    public void ToggleTrainingDummies( )
    {
        m_dummies.SetActive( !m_dummies.activeSelf );
    }

    public void ToggleHoloSlug()
    {
        m_holoSlug.SetActive( !m_holoSlug.activeSelf );
    }

}
