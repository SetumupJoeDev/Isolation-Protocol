using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioLogPlayer : MonoBehaviour
{

    public AudioSource m_playbackSource;

    public Slider m_playbackTimeline;

    private bool m_hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if( m_playbackSource.isPlaying )
        {

            if ( !m_hasPlayed )
            {
                m_hasPlayed = true;
                m_playbackTimeline.value = 0;
            }

            float percentagePlayedSinceUpdate = ( Time.deltaTime / m_playbackSource.clip.length ) * 100 ;

            m_playbackTimeline.value += percentagePlayedSinceUpdate;

        }
        else if( m_hasPlayed )
        {
            m_playbackTimeline.value = 100;

            m_hasPlayed = false;

        }

    }
}
