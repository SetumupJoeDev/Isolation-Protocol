using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : CanvasController
{

    public float m_fadeIncrement;

    public float m_fadeIncrementDelay = 0.015f;

    private void Start( )
    {
        gameEvents.hello.levelLoadingComplete += FadeCanvasOut;
    }

    public IEnumerator FadeIn( GameStartDoor gameStart )
    {
        while ( m_canvasGroup.alpha < 1 )
        {
            m_canvasGroup.alpha += m_fadeIncrement;

            yield return new WaitForSeconds( m_fadeIncrementDelay );

        }

        m_canvasGroup.blocksRaycasts = true;

        gameStart.LoadLevel( );

    }

    public void FadeCanvasOut( )
    {
        StartCoroutine( FadeOut( ) );
    }

    public IEnumerator FadeOut( )
    {
        while ( m_canvasGroup.alpha > 0 )
        {
            m_canvasGroup.alpha -= m_fadeIncrement;

            yield return new WaitForSeconds( m_fadeIncrementDelay );

        }

        m_canvasGroup.blocksRaycasts = false;
    }
}
