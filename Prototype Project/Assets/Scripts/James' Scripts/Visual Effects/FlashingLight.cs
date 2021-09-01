using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{

    [Tooltip("The 2D light associated with this script.")]
    public Light2D m_light;

    [Tooltip("Determines whether or not this light should flicker.")]
    public bool m_shouldFlicker;

    [Tooltip("The time between the light turning off and back on again.")]
    public float m_maxFlickerDuration;

    [Tooltip("The maximum amount of time the light will stay on before flickering again.")]
    public float m_maxTimeBetweenFlickers;

    [Tooltip("The sound that the light plays while it is on.")]
    public AudioSource m_lightSound;

    private void Start( )
    {

        //Finds and assigns the light attached to this object
        m_light = GetComponent<Light2D>( );

        //If the light should flicker, then the flickering coroutine is started
        if ( m_shouldFlicker )
        {
            StartCoroutine( FlickerLight( ) );
        }

    }

    public IEnumerator FlickerLight( )
    {

        //Generates random values for the time between flickers and the duration of the flicker (the amount of time the light stays off), between 0 and the value of the max flicker duration and time between flickers
        float flickerDuration = Random.Range( 0 , m_maxFlickerDuration );

        float timeBetweenFlickers = Random.Range( 0, m_maxTimeBetweenFlickers );

        //Enables the light, allowing it to emit light
        m_light.enabled = true;

        //Plays the sound the light makes while it is on
        m_lightSound.Play( );

        //Waits for the duration of the time between flickers generated above
        yield return new WaitForSeconds( timeBetweenFlickers );

        //Disables the light, preventing it from emitting light
        m_light.enabled = false;

        //Pauses the sound the light makes while it is on
        m_lightSound.Pause( );

        //Waits for the duration of the flicker generated above
        yield return new WaitForSeconds( flickerDuration );

        //Restarts the coroutine to loop the flickering
        StartCoroutine( FlickerLight( ) );

    }

}
