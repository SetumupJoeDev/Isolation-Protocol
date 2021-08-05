using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDummy : MonoBehaviour
{

    private bool m_hasBeenHit;

    public AudioSource m_hitSound;

    public SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>( );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ProjectileBase>() != null && !m_hasBeenHit )
        {
            m_hasBeenHit = true;
            m_hitSound.Play( );
            m_spriteRenderer.color = Color.green;
            if ( TutorialEventListener.current != null )
            {
                TutorialEventListener.current.DummyHit();
            }
        }
    }

}
