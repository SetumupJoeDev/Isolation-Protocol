using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestationPod : MonoBehaviour
{

    [Tooltip("An array of Scriptable Objects that are used to spawn enemies from the pod.")]
    public GestationPodSpawnEvent[] m_spawnEvents;

    //An integer representing the array index of the enemy spawn event that will be used
    private int m_spawnEventIndex;

    [Tooltip("The interval between enemy spawn events.")]
    public float m_spawnEventInterval;

    [Tooltip("A boolean that determines whether or not the gestation pod can currently spawn enemies.")]
    public bool m_canTriggerSpawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        //Loops through the array of spawn events and sets the spawning position of the enemies to the position of the pod
        foreach( GestationPodSpawnEvent spawnEvent in m_spawnEvents )
        {
            spawnEvent.m_eggPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the pod can spawn enemies, the spawning coroutine is started
        if( m_canTriggerSpawnEvent )
        {
            StartCoroutine( TriggerSpawnEvent( ) );
        }
    }

    public IEnumerator TriggerSpawnEvent( )
    {

        //Sets this to false so that the coroutine cannot be called more than once before it ends
        m_canTriggerSpawnEvent = false;

        //Triggers the spawn event at the current array index
        m_spawnEvents[m_spawnEventIndex].SpawnEvent( );

        //Increments the value of the spawn index so a new one is used next time
        m_spawnEventIndex++;

        //If the value of the spawn event index is outside of the array, it is reset to zero to avoid errors
        if(m_spawnEventIndex >= m_spawnEvents.Length )
        {
            m_spawnEventIndex = 0;
        }

        //Waits for the duration of the spawning interval before allowing another spawn event to happen
        yield return new WaitForSeconds( m_spawnEventInterval );

        //Sets this to true so that another spawning event can occur
        m_canTriggerSpawnEvent = true;

    }

}
