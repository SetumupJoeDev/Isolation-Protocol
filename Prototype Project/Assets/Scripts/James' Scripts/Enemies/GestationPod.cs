using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestationPod : MonoBehaviour
{

    public GestationPodSpawnEvent[] m_spawnEvents;

    public int m_spawnEventIndex;

    public float m_spawnEventInterval;

    public bool m_canTriggerSpawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        foreach( GestationPodSpawnEvent spawnEvent in m_spawnEvents )
        {
            spawnEvent.m_eggPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( m_canTriggerSpawnEvent )
        {
            StartCoroutine( TriggerSpawnEvent( ) );
        }
    }

    public IEnumerator TriggerSpawnEvent( )
    {

        m_canTriggerSpawnEvent = false;

        m_spawnEvents[m_spawnEventIndex].SpawnEvent( );

        m_spawnEventIndex++;

        if(m_spawnEventIndex >= m_spawnEvents.Length )
        {
            m_spawnEventIndex = 0;
        }

        yield return new WaitForSeconds( m_spawnEventInterval );

        m_canTriggerSpawnEvent = true;

    }

}
