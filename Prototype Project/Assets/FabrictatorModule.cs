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

    public override void Activated( )
    {
        m_fabricatorCanvas.ToggleCanvas( );

        m_playerController.GetComponentInChildren<HUDManager>( ).gameObject.SetActive( false );

    }

    public override void ToggleHighlighting( bool highlightActive )
    {

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
