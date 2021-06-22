using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeHealer : MonoBehaviour
{

    [Header("Healing")]

    public float m_healingRange;

    public float m_healingInterval;

    public LayerMask m_enemyLayer;

    public int m_healAmount;

    public bool m_canHeal;

    // Update is called once per frame
    void Update()
    {
        if ( m_canHeal )
        {
            StartCoroutine( HealEnemies( ) );
        }
    }

    public IEnumerator HealEnemies( )
    {

        m_canHeal = false;

        Collider2D[] enemiesToHeal = Physics2D.OverlapCircleAll( transform.position, m_healingRange, m_enemyLayer ); 

        if(enemiesToHeal.Length > 0 )
        {
            foreach( Collider2D enemy in enemiesToHeal )
            {
                enemy.gameObject.GetComponent<HealthManager>( ).Heal( m_healAmount );
            }
        }

        yield return new WaitForSeconds( m_healingInterval );

        m_canHeal = true;

    }

}
