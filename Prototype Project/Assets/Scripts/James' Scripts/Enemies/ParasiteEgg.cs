using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteEgg : MonoBehaviour
{

    [Header("Enemy Spawning")]

    [Tooltip("The number of enemies that this egg should spawn.")]
    public GameObject[] m_enemiesToSpawn;

    [Tooltip("The sound that plays when the egg hatches.")]
    public AudioClip m_hatchingSound;

    public analyticsManager m_enemyCounter; // Lewis' code


    public void Start()
    {
        m_enemyCounter = GameObject.Find("easyName").GetComponent<analyticsManager>(); //Lewis' code
    }


    public void OnTriggerEnter2D( Collider2D collision )
    {
        //Lewis' code
        if (m_enemyCounter != null)
        {
            m_enemyCounter.parasiteEggKilled++;
        }
        //End of Lewis' code

        //Hatches an enemy when the player "steps on" the egg
        HatchEnemy( );
    }

    public void HatchEnemy()
    {
       
        //Generates a random index to use in spawning a random enemy type from the array
        int enemyIndex = Random.Range(0, m_enemiesToSpawn.Length - 1);
        
        //Instantiates a new enemy using the above index at the position of the egg
        Instantiate( m_enemiesToSpawn[enemyIndex] , transform.position , Quaternion.identity );

        //Plays the hatching sound to inform the player that the egg has hatched
        AudioSource.PlayClipAtPoint( m_hatchingSound , transform.position );

        //Destroys the egg so it can only hatch once
        Destroy( gameObject );

    }

}
