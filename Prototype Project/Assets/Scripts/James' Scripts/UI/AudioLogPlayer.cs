using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioLogPlayer : MonoBehaviour
{

    public AudioSource m_playbackSource;

    public Slider m_playbackTimeline;

    private bool m_hasPlayed;

    private bool m_wasStopped;


    // Update is called once per frame
    void Update()
    {
        //IF the AudioSource is playing, the value of the timeline slider is incremented by percentage of the clip that has played since the last frame
        if( m_playbackSource.isPlaying )
        {
            //If the audiolog has already played, then the timeline value is set back to 0
            if ( !m_hasPlayed )
            {
                m_hasPlayed = true;
                m_playbackTimeline.value = 0;
            }

            //Calculates the percentage of the log that has played since the last frame by dividing the value of deltaTime by the length of the clip
            float percentagePlayedSinceUpdate = ( Time.deltaTime / m_playbackSource.clip.length ) * 100 ;

            //Increases the slider's value by the value calculated above to indicate to the player how much of the log has been played
            m_playbackTimeline.value += percentagePlayedSinceUpdate;

        }
        //Otherwise, if the log has played but isn't currently playing, the timeline value is set to 100 to show the player that the clip has ended
        else if( m_hasPlayed && !m_wasStopped )
        {
    
            m_playbackTimeline.value = 100;

            m_hasPlayed = false;

        }
    }

    public void PlayButtonPressed( )
    {
        if ( m_playbackSource.isPlaying )
        {
            m_playbackSource.Stop( );
            m_playbackTimeline.value = 0;
            m_wasStopped = true;
        }
        else
        {
            m_playbackSource.Play( );
            m_wasStopped = false;
        }
    }
}
