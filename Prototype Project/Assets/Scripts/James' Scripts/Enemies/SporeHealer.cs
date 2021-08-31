using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeHealer : MonoBehaviour
{

    [Header("Healing")]

    [Tooltip("The range in which this enemy can heal other enemies.")]
    public float m_healingRange;

    [Tooltip("The interval between healing effects.")]
    public float m_healingInterval;

    [Tooltip("The physics layer that the enemies sit on. Used to find enemies to heal.")]
    public LayerMask m_enemyLayer;

    [Tooltip("The amount of health this enemy restores to other enemies.")]
    public int m_healAmount;

    [Tooltip("A boolean that determines whether or not this enemy can currently heal other enemies.")]
    public bool m_canHeal;

    // Update is called once per frame
    void Update()
    {
        //If the enemy can heal, the healing coroutine is started
        if ( m_canHeal )
        {
            StartCoroutine( HealEnemies( ) );
        }
    }

    public IEnumerator HealEnemies( )
    {

        //Sets this to false so the coroutine can only be run once before finishing
        m_canHeal = false;

        //Uses an overlap circle to generate an array of enemies within the healing range
        Collider2D[] enemiesToHeal = Physics2D.OverlapCircleAll( transform.position, m_healingRange, m_enemyLayer ); 

        //If there are any enemies in the array, the code loops through them and heals each of them
        if(enemiesToHeal.Length > 0 )
        {
            foreach( Collider2D enemy in enemiesToHeal )
            {
                enemy.gameObject.GetComponent<HealthManager>( ).Heal( m_healAmount );
            }
        }

        //Waits for the duration of the healing interval before setting canHeal to true again
        yield return new WaitForSeconds( m_healingInterval );

        //Sets this to true so that the healing coroutine can be called again
        m_canHeal = true;

    }

}
