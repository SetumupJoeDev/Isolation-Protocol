using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public bool m_isBeingLookedAt;

    [HideInInspector]
    public bool m_highlightingActive;

    public PlayerController m_playerController;

    [SerializeField]
    private GameObject m_interactionPrompt;

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
        
    }

    public virtual void ToggleHighlighting( bool highlightActive )
    {

        m_interactionPrompt.SetActive( highlightActive );

        m_highlightingActive = highlightActive;

    }

}
