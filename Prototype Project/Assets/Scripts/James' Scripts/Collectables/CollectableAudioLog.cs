using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAudioLog : InteractableObject
{

    [Tooltip("The sprite to switch to when this object is highlighted.")]
    public Sprite m_highlightedSprite;
    
    [Tooltip("The object's default sprite.")]
    public Sprite m_defaultSprite;
    
    [Tooltip("The sprite renderer component attached to this object.")]
    public SpriteRenderer m_spriteRenderer;
    
    [Tooltip("The total number of audiologs the player has collected.")]
    public AudioClip m_logCollected;

    public override void Activated( PlayerController playerController )
    {
        //Unlocks a new audiolog in the player's audilog list
        m_playerController.m_audioLogList.UnlockNewLog( );

        //Plays a sound to indicate to the player that they have collected the log, before destroying the object
        AudioSource.PlayClipAtPoint( m_logCollected , transform.position );

        Destroy( gameObject );

    }

    public override void ToggleHighlighting( bool highlightActive )
    {
        //Switches between the item's highlighted sprite depending on whether or not it is currently highlighted
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
