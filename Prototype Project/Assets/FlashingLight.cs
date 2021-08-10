using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{

    public Light2D m_light;

    [Tooltip("The time between the light turning off and back on again.")]
    public float m_maxFlickerDuration;

    public float m_maxTimeBetweenFlickers;


    private void Start( )
    {

        m_light = GetComponent<Light2D>( );

        StartCoroutine( FlickerLight( ) );

    }

    public IEnumerator FlickerLight( )
    {

        float flickerDuration = Random.Range( 0 , m_maxFlickerDuration );

        float timeBetweenFlickers = Random.Range( 0, m_maxTimeBetweenFlickers );

        m_light.enabled = true;

        yield return new WaitForSeconds( flickerDuration );

        m_light.enabled = false;

        yield return new WaitForSeconds( timeBetweenFlickers );

        StartCoroutine( FlickerLight( ) );

    }

}
