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

    [SerializeField]
    private AudioClip m_logCollected;

    public override void Activated( )
    {
        m_playerController.m_audioLogList.UnlockNewLog( );

        AudioSource.PlayClipAtPoint( m_logCollected , transform.position );

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
