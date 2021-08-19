using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    //A boolean that determines whether or not the object is currently being looked at by the player
    [HideInInspector]
    public bool m_isBeingLookedAt;

    //A boolean that determines whether or not the object's highlighting is currently active
    [HideInInspector]
    public bool m_highlightingActive;

    [Tooltip("The PlayerController script attached to the player interacting with the object.")]
    public PlayerController m_playerController;

    //The UI Object displayed to the player to inform them how to interact wth the object
    [SerializeField]
    private GameObject m_interactionPrompt;

    [Tooltip("A boolean that determines whether or not this object has been activated by the player.")]
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
