using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys spawn points that collide with it to avoid room overlap
public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            Destroy(collision.gameObject);
        }
    }
}
