using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    [Header("Sound")]
    [Tooltip("The sound that plays when the object is collected.")]
    public AudioClip m_collectedSound;

    protected void OnTriggerEnter2D( Collider2D collision )
    {
        //If the object has collided with the player, then a sound is played and GetCollected is called, with the player's script as a parameter
        if ( collision.gameObject.GetComponent<PlayerController>() != null )
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            AudioSource.PlayClipAtPoint( m_collectedSound , transform.position );
            GetCollected( player );
        }
    }

    public virtual void GetCollected( PlayerController player )
    {
        Destroy( gameObject );
    }

}
