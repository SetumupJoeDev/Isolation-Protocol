using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    [Header("Sound")]
    [Tooltip("The sound that plays when the object is collected.")]
    public AudioClip m_collectedSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.gameObject.tag == "Player" )
        {
            PlayerDemo player = collision.gameObject.GetComponent<PlayerDemo>();
            GetCollected( player );
            AudioSource.PlayClipAtPoint( m_collectedSound , transform.position );
            Destroy( gameObject );
        }
    }

    public virtual void GetCollected( PlayerDemo player )
    {
        Destroy( gameObject );
    }

}
