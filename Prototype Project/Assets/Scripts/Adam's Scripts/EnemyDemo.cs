using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemo : CharacterBase
{
    protected int health = 3;

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = -1;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            health--;
        }
    }
}
