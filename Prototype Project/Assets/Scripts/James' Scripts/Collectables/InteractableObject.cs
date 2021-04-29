using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public bool m_isBeingLookedAt;

    [HideInInspector]
    public bool m_highlightingActive;

    public Light m_light;

    public PlayerController m_playerController;

    public virtual void Update( )
    {
        if( m_isBeingLookedAt && !m_highlightingActive )
        {
            ToggleHighlighting( true );
        }
        if( !m_isBeingLookedAt && m_highlightingActive )
        {
            ToggleHighlighting( false );
        }

        m_isBeingLookedAt = false;

        m_playerController = null;

    }

    public virtual void Activated( )
    {
        Debug.Log( "I was activated!" );
    }

    public virtual void ToggleHighlighting( bool highlightActive )
    {

        Debug.Log( "Boop!" );

        m_highlightingActive = highlightActive;

    }

}
