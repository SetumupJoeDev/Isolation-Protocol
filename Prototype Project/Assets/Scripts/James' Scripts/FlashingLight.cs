using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{

    public Light2D m_light;

    [Tooltip("Determines whether or not this light should flicker.")]
    public bool m_shouldFlicker;

    [Tooltip("The time between the light turning off and back on again.")]
    public float m_maxFlickerDuration;

    [Tooltip("The maximum amount of time the light will stay on before flickering again.")]
    public float m_maxTimeBetweenFlickers;

    public AudioSource m_lightSound;

    private void Start( )
    {

        m_light = GetComponent<Light2D>( );

        if ( m_shouldFlicker )
        {
            StartCoroutine( FlickerLight( ) );
        }

    }

    public IEnumerator FlickerLight( )
    {

        float flickerDuration = Random.Range( 0 , m_maxFlickerDuration );

        float timeBetweenFlickers = Random.Range( 0, m_maxTimeBetweenFlickers );

        m_light.enabled = true;

        m_lightSound.Play( );

        yield return new WaitForSeconds( timeBetweenFlickers );

        m_light.enabled = false;

        m_lightSound.Pause( );

        yield return new WaitForSeconds( flickerDuration );

        StartCoroutine( FlickerLight( ) );

    }

}
