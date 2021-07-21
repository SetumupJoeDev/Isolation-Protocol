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

    public bool m_isActivated;

    public virtual void Update( )
    {
        if ( !m_isActivated )
        {
            //If the object is being looked at, and its highlighting is not active, the highlighting is toggled on
            if ( m_isBeingLookedAt && !m_highlightingActive )
            {
                ToggleHighlighting( true );
            }
            //Otherwise, it is toggled off
            if ( !m_isBeingLookedAt && m_highlightingActive )
            {
                ToggleHighlighting( false );
            }

            //These are reset each update to determine whether or not the player is looking away
            m_isBeingLookedAt = false;

            m_playerController = null;
        }

    }

    public virtual void Activated( PlayerController playerController )
    {
        //Activation logic goes here
        m_isActivated = !m_isActivated;

        m_playerController = playerController;

    }

    public virtual void ToggleHighlighting( bool highlightActive )
    {
        //Activates the object's attached interaction prompt, telling the player how to use this object
        m_interactionPrompt.SetActive( highlightActive );

        m_highlightingActive = highlightActive;

    }

}
