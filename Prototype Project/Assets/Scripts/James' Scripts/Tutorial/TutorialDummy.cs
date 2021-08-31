using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDummy : MonoBehaviour
{

    //A boolean that determines whether or not this dummy has been shot yet
    private bool m_hasBeenHit;

    [Tooltip("The sound that plays when the dummy is hit by a projectile.")]
    public AudioSource m_hitSound;

    [Tooltip("The sprite renderer attached to the dummy.")]
    public SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        //Finds and assigns the sprite renderer attached to the dummy
        m_spriteRenderer = GetComponent<SpriteRenderer>( );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the dummy is hit by a projectile and hasn't been before, hasBeenHit is set to true, the hit sound is played and the dummy's colour changes to green
        if(collision.GetComponent<ProjectileBase>() != null && !m_hasBeenHit )
        {
            m_hasBeenHit = true;
            m_hitSound.Play( );
            m_spriteRenderer.color = Color.green;
            //Calls the DummyHit event on the tutorial event listener to update the task counter
            if ( TutorialEventListener.current != null )
            {
                TutorialEventListener.current.DummyHit();
            }
        }
    }

}
