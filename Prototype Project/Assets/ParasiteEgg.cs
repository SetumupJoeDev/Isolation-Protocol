using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteEgg : MonoBehaviour
{

    [Header("Enemy Spawning")]
    public GameObject[] m_enemiesToSpawn;

    public AudioClip m_hatchingSound;


    public void OnTriggerEnter2D( Collider2D collision )
    {
        HatchEnemy( );
    }

    public void HatchEnemy( )
    {

        int enemyIndex = Random.Range( 0, m_enemiesToSpawn.Length - 1 );

        Instantiate( m_enemiesToSpawn[enemyIndex] , transform.position , Quaternion.identity );

        AudioSource.PlayClipAtPoint( m_hatchingSound , transform.position );

        Destroy( gameObject );

    }

}
