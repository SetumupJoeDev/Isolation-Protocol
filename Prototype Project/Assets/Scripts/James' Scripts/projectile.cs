using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public Vector2 velocity;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;

        StartCoroutine( WaitToDestroy( ) );

    }

    public IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds( 5 );

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        Destroy( gameObject );
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( velocity.normalized * speed * Time.deltaTime );
    }
}
