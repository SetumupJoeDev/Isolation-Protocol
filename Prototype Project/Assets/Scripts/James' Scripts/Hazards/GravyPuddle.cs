using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravyPuddle : MonoBehaviour
{

    [Tooltip("The amount by which the player's slowness debuff is altered by the gravy puddle.")]
    public float m_speedDebuff;

    [Tooltip("The amount of time for which the player is slowed by the gravy puddle.")]
    public float m_slownessDuration;

    [Tooltip("The amount of time that passes before the gravy puddle disperses.")]
    public float m_puddleLifetime;

    private void Start( )
    {
        //Starts the coroutine to destroy the object after a certain peroid of time
        StartCoroutine( WaitToDestroy( ) );
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        //If the object that collided with the puddle has a CharacterBase attached, it is assigned as a local variable
        CharacterBase affectedCharacter = collision.GetComponent<CharacterBase>();

        //If the above variable is not null and is not slowed by a hazard, then the slowness coroutine in the character is started
        if ( affectedCharacter != null && !affectedCharacter.m_slowedByHazard )
        {
            StartCoroutine( affectedCharacter.TemporarySlowness( m_slownessDuration , m_speedDebuff ) );

            //If the affected character is the player, their dash ability is also temporarily disabled
            if( affectedCharacter.GetComponent<PlayerController>() != null )
            {
                StartCoroutine( affectedCharacter.GetComponent<PlayerController>( ).DisableDash( m_slownessDuration ) );
            }

        }
    }

    public IEnumerator WaitToDestroy( )
    {

        //Waits for the duration of the puddle's lifetime before triggering its shrink animation
        yield return new WaitForSeconds( m_puddleLifetime );

        GetComponent<Animator>( ).SetTrigger("Shrink");

    }

    public void DisableCollisions( )
    {
        //Disables collisions on the attached boxcollider
        GetComponent<BoxCollider2D>( ).enabled = false;
    }

    public void DestroyPuddle( )
    {
        //Destroys the gameObject in an event called at the end of the shrink animation
        Destroy( gameObject );
    }

}
