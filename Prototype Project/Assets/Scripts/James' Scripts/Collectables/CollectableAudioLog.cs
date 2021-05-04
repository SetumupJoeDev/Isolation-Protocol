using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAudioLog : InteractableObject
{

    [SerializeField]
    private Sprite m_highlightedSprite;

    [SerializeField]
    private Sprite m_defaultSprite;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    public override void Activated( )
    {
        m_playerController.m_audioLogList.UnlockNewLog( );

        Destroy( gameObject );

    }

    public override void ToggleHighlighting( bool highlightActive )
    {

        if( highlightActive )
        {
            m_spriteRenderer.sprite = m_highlightedSprite;
        }
        else
        {
            m_spriteRenderer.sprite = m_defaultSprite;
        }

        base.ToggleHighlighting( highlightActive );
    }

}
