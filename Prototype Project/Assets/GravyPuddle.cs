using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravyPuddle : MonoBehaviour
{

    public float m_speedDebuff;

    public float m_slownessDuration;

    public float m_puddleLifetime;

    private void Start( )
    {
        StartCoroutine( WaitToDestroy( ) );
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {

        CharacterBase affectedCharacter = collision.GetComponent<CharacterBase>();

        if ( affectedCharacter != null && !affectedCharacter.m_slowedByHazard )
        {
            StartCoroutine( affectedCharacter.TemporarySlowness( m_slownessDuration , m_speedDebuff ) );

            if( affectedCharacter.GetComponent<PlayerController>() != null )
            {
                StartCoroutine( affectedCharacter.GetComponent<PlayerController>( ).DisableDash( m_slownessDuration ) );
            }

        }
    }

    public IEnumerator WaitToDestroy( )
    {

        yield return new WaitForSeconds( m_puddleLifetime );

        GetComponent<Animator>( ).SetTrigger("Shrink");

    }

    public void DisableCollisions( )
    {
        GetComponent<BoxCollider2D>( ).enabled = false;
    }

    public void DestroyPuddle( )
    {
        Destroy( gameObject );
    }

}
