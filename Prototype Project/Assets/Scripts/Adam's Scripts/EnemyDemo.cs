using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemo : CharacterBase
{
    [SerializeField]
    protected int health = 3;

    // Update is called once per frame
    void Update()
    {
        m_directionalVelocity.x = -1;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health--;
        }
    }
}
