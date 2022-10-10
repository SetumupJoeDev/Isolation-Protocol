using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrictatorModule : InteractableObject
{
    [Tooltip("The canvas controller attached to the UI canvas of the fabricator store.")]
    public CanvasController m_fabricatorCanvas;

    [Tooltip("The sprite renderer attached to the fabricator module.")]
    public SpriteRenderer m_spriteRenderer;

    [Tooltip("The default sprite of the fabricator.")]
    public Sprite m_defaultSprite;

    [Tooltip("The highlighted version of the fabricator's sprite.")]
    public Sprite m_highlightedSprite;

    [Tooltip("The sound that plays when the player opens a UI window.")]
    public AudioSource m_windowOpen;

    [Tooltip("The sound that plays when the player closes a UI window.")]
    public AudioSource m_windowClose;

    [Tooltip("The sound that plays when the player changes to a different UI tab.")]
    public AudioSource m_tabClose;

    public override void Activated( PlayerController playerController )
    {
        //Toggles the fabricator's canvas to make it visible to the player
        m_fabricatorCanvas.ToggleCanvas( );

        //Deactivates the player's HUD so that it is not displayed over the fabricator UI
        m_playerController.ToggleHUD( );

        //Sets the player as in a menu, so they cannot move or shoot
        m_playerController.m_isInMenu = true;

        //Plays the sound of the UI window opening
        m_windowOpen.Play( );
    }

    public void PlayWindowCloseSound( )
    {
        //Plays the sound of the UI window closing
        m_windowClose.Play( );
    }

    public void PlayTabCloseSound( )
    {
        //Plays the sound of the UI tab changing
        m_tabClose.Play( );
    }

    public override void ToggleHighlighting( bool highlightActive )
    {
        //Toggles between the higlighted and default sprite depending on the value passed in
        if ( !highlightActive )
        {
            m_spriteRenderer.sprite = m_defaultSprite;
        }
        else
        {
            m_spriteRenderer.sprite = m_highlightedSprite;
        }

        base.ToggleHighlighting( highlightActive );
    }

}
