using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{

    [Header("Booleans")]

    [Tooltip("A boolean that determines whether or not the task is active.")]
    public bool m_isTaskActive;

    [Tooltip("A boolean that determines whether or not the tutorial has been completed.")]
    public bool m_tutorialComplete;

    [Header("Tasks")]

    [Tooltip("The list of tasks that must be completed throughout the tutorial.")]
    public List<TutorialTask> m_tutorialTasks;

    [Tooltip("The tutorial task that is currently active.")]
    public TutorialTask m_currentTask;

    [Header("Audio")]

    [Tooltip("The voice clips that play during the tutorial.")]
    public AudioClip[] m_voiceClips;

    [Tooltip("The audiosource that plays the tutorial voice clips.")]
    public AudioSource m_voiceLinePlayer;

    [Header("Dummy Enemies")]

    [Tooltip("The dummies used in the aiming and firing portion of the tutorial")]
    public GameObject m_dummies;

    [Tooltip("The holographic HoloSlug used in the grapple counter portion of the tutorial.")]
    public GameObject m_holoSlug;

    [Header("Indices")]

    [Tooltip("The array index of the currently active task.")]
    public int m_currentTaskIndex = 0;

    [Tooltip("The array index of the current voice clip.")]
    public int m_voiceLineIndex = 0;

    [Header("User Interface")]

    [Tooltip("The canvas controller attached to the tutorial menu.")]
    public CanvasController m_menuCanvasController;

    [Tooltip("The UI Text element that displays the name of the current task.")]
    public TextMeshProUGUI m_taskName;

    [Tooltip("The UI Text element that displays the description of the current task.")]
    public TextMeshProUGUI m_taskDescription;
    
    [Tooltip("The UI Text element that displays the name of the current task's tracked variable.")]
    public TextMeshProUGUI m_taskVariableName;
    
    [Tooltip("The UI Text element that displays the value of the current task's tracked variable.")]
    public TextMeshProUGUI m_taskCounter;

    [Header("Player")]

    [Tooltip("The PlayerController attached to the player in the tutorial.")]
    public PlayerController m_playerController;

    private void Start( )
    {

        //Subscribes the ToggleTrainingDummies method to the OnAimTaskStart method
        TutorialEventListener.current.OnAimTaskStart += ToggleTrainingDummies;

        //Subscribes the ToggleTrainingDummies method to the OnAimTaskEnd method
        TutorialEventListener.current.OnAimTaskEnd += ToggleTrainingDummies;

        //Subscribes the ToggleHoloSlug method to the OnGrappleTaskStart method
        TutorialEventListener.current.OnGrappleTaskStart += ToggleHoloSlug;

        //Adds the various tutorial tasks to the list of tutorial tasks
        m_tutorialTasks.Add( new MovementTask( ) );

        m_tutorialTasks.Add( new DodgeTask( ) );

        m_tutorialTasks.Add( new AimTask( ) );

        m_tutorialTasks.Add( new GrappleCounterTask( ) );

        //Loops through each task and initialises them
        foreach(TutorialTask task in m_tutorialTasks )
        {
            task.Initialise( );
        }

        //Sets the current task as the first in the list
        m_currentTask = m_tutorialTasks[ m_currentTaskIndex ];

        //Sets the audio clip of the voice line player as the first in the array
        m_voiceLinePlayer.clip = m_voiceClips[ m_voiceLineIndex ];

        //Starts the coroutine to wait for the end of the voice clip before starting the first task
        StartCoroutine( WaitForVoiceClip( ) );
    }

    public void StartNewTask( )
    {

        //Updates the Tutorial UI to reflect the new task
        m_taskName.text = m_currentTask.m_taskTitle;

        m_taskDescription.text = m_currentTask.m_taskDescription;

        m_taskVariableName.text = m_currentTask.m_taskVariableName;

        m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString();

        //Starts the new task
        m_currentTask.StartTask( );

    }

    public void Update( )
    {

        //If the tutorial isn't complete, we check for the player's progress in the current task
        if ( !m_tutorialComplete )
        {

            if ( m_isTaskActive )
            {
                //Checks the player's progress in the current task
                m_currentTask.m_taskGoal.CheckObjectiveProgress( );

                //Updates the value of the task counter text to the value of the task's goal's currentAmount
                m_taskCounter.text = m_currentTask.m_taskGoal.m_currentAmount.ToString( );
            }

            //If the task's goal has been achieved, the current task index is incremented to start a new task
            if ( m_currentTask.m_taskGoal.GoalAchieved( ) )
            {
                m_currentTaskIndex++;

                //If the task index is now outside of the array, then the tutorial is complete
                if ( m_currentTaskIndex >= m_tutorialTasks.Count )
                {
                    m_tutorialComplete = true;
                }
                //Otherwise, the currentTask is updated
                else
                {
                    m_currentTask = m_tutorialTasks[m_currentTaskIndex];
                }

                //Waits for the newxt voice clip to finish playing before starting a new task
                StartCoroutine( WaitForVoiceClip( ) );
            }
        }

    }

    public IEnumerator WaitForVoiceClip( )
    {

        //Sets this to false to prevent the UI from being updated between tasks
        m_isTaskActive = false;

        //Plays the current tutorial voice clip
        m_voiceLinePlayer.Play( );

        //Incremements the value of the voice line index to play the next in the array next time the coroutine is called
        m_voiceLineIndex++;

        //Waits for the duration of the voice clip before moving on
        yield return new WaitForSeconds( m_voiceLinePlayer.clip.length );

        //If the tutorial isn't complete, then the next voice clip in the array is assigned to the audio source and a new task is started
        if ( !m_tutorialComplete )
        {
            m_voiceLinePlayer.clip = m_voiceClips[m_voiceLineIndex];

            StartNewTask( );

            m_isTaskActive = true;
        }
        //Otherwise, the tutorial menu opens so the player can return to the menu
        else
        {
            m_menuCanvasController.ToggleCanvas( );
        }

    }

    public void ToggleTrainingDummies( )
    {
        //Toggles the training dummies on or off
        m_dummies.SetActive( !m_dummies.activeSelf );
    }

    public void ToggleHoloSlug()
    {
        //Activates the Holo Slug
        m_holoSlug.SetActive( !m_holoSlug.activeSelf );
    }

}
