using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    
    [Tooltip("The canvas group component attached to this object that this script will control.")]
    public CanvasGroup m_canvasGroup;

    [Tooltip("A boolean that determines whether or not the canvas is currently active.")]
    public bool m_isCanvasActive = false;

    public void ToggleCanvas( )
    {
        //Shows or hides the canvas depending on whether or not the canvas is currently active
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
        //Sets the alpha of the canvas group to 1 so that it is fully opaque and visible
        m_canvasGroup.alpha = 1;

        //Sets the canvas group to block raycasts so it can be interacted with
        m_canvasGroup.blocksRaycasts = true;

        //Sets the canvas to active so it can be deactivated again
        m_isCanvasActive = true;

    }

    private void HideCanvas( )
    {
        //Sets the alpha of the canvas group to 0 to make it fully transparent
        m_canvasGroup.alpha = 0;

        //Sets the canvas group to not block raycasts so it can't be interacted with
        m_canvasGroup.blocksRaycasts = false;

        //Sets the canvas to inactive so it can be reactivated again
        m_isCanvasActive = false;

    }
}
