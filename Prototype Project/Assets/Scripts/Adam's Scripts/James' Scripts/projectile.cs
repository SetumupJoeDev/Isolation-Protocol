using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public Vector2 velocity;

    public float speed;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine( WaitToDestroy( ) );

    }

    public IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds( 5 );

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.gameObject.GetComponent<HealthManager>() != null )
        {
            collision.gameObject.GetComponent<HealthManager>( ).TakeDamage( damage );
        }
        Destroy( gameObject );
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( velocity.normalized * speed * Time.deltaTime );
    }
}
