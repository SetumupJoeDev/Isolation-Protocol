using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrictatorModule : InteractableObject
{
    [SerializeField]
    private CanvasController m_fabricatorCanvas;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    [SerializeField]
    private Sprite m_defaultSprite;

    [SerializeField]
    private Sprite m_highlightedSprite;

    public override void Activated( PlayerController playerController )
    {
        //Toggles the fabricator's canvas to make it visible to the player
        m_fabricatorCanvas.ToggleCanvas( );

        //Deactivates the player's HUD so that it is not displayed over the fabricator UI
        m_playerController.ToggleHUD( );

        //Sets the player as in a menu, so they cannot move or shoot
        m_playerController.m_isInMenu = true;

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
