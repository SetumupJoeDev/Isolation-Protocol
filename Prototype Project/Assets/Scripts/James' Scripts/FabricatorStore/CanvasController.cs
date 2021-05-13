using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_canvasGroup;

    private bool m_isCanvasActive = false;

    public void ToggleCanvas( )
    {
        if ( !m_isCanvasActive )
        {
            ShowCanvas( );
        }
        else
        {
            HideCanvas( );
        }
    }

    private void ShowCanvas( )
    {

        m_canvasGroup.alpha = 1;

        m_canvasGroup.blocksRaycasts = true;

        m_isCanvasActive = true;

    }

    private void HideCanvas( )
    {
        m_canvasGroup.alpha = 0;

        m_canvasGroup.blocksRaycasts = false;

        m_isCanvasActive = false;

    }

}
