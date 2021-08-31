using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : CanvasController
{

    [Tooltip("The amount by which the canvas' opcaity is incremented each time it is changed.")]
    public float m_fadeIncrement;

    [Tooltip("The delay between the loading screen canvas' opacity being changed.")]
    public float m_fadeIncrementDelay = 0.015f;

    private void Start( )
    {
        //Subscribes the FadeCanvasIn method to the loadingNewLevel event
        gameEvents.hello.loadingNewLevel += FadeCanvasIn;

        //Subscribes the FadeCanvasOut method to the levelLoadingComplete event
        gameEvents.hello.levelLoadingComplete += FadeCanvasOut;
    }

    public IEnumerator FadeIn( LevelLoader levelLoader, string newLevelName )
    {
        //While the canvas' alpha is less than 1, it is increased by the value of fadeIncrement
        while ( m_canvasGroup.alpha < 1 )
        {
            m_canvasGroup.alpha += m_fadeIncrement;

            //Waits for the duration of the fadeIncrementDelay before altering the opacity further
            yield return new WaitForSeconds( m_fadeIncrementDelay );

        }

        //Once the opacity is 1 or more, this is set to true to prevent any possible UI elements beneath it from being clicked
        m_canvasGroup.blocksRaycasts = true;

        //Loads the level passed in through the parameters
        levelLoader.LoadNewLevel( newLevelName );

    }

    public void FadeCanvasIn( LevelLoader levelLoader, string newLevelName )
    {
        //Calls the fading in coroutine, passing in the level loader and the scene name to load so the new level can be loaded
        StartCoroutine( FadeIn( levelLoader, newLevelName ) );
    }

    public void FadeCanvasOut( )
    {
        //Starts the fading out coroutine
        StartCoroutine( FadeOut( ) );
    }

    public IEnumerator FadeOut( )
    {
        //While the canvas' alpha is greater than 0, it is reduced by the value of fadeIncrement
        while ( m_canvasGroup.alpha > 0 )
        {
            m_canvasGroup.alpha -= m_fadeIncrement;

            //Waits for the duration of the fadeIncrementDelay before altering the opacity further
            yield return new WaitForSeconds( m_fadeIncrementDelay );

        }

        //Once the opacity is 0 or less, this is set to false to allow any UI elements beneath it to be clicked
        m_canvasGroup.blocksRaycasts = false;
    }
}
