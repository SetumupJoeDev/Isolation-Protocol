using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    SpriteRenderer m_spriteRenderer;// Lewis' code
    public float m_damagedTime = 0.15f; // Lewis's code.
    public float m_damagedAmount = 1f;
    

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    } // Whole start method Lewis' code


    private void Update()
    {

        if (m_currentHealth <= m_maxHealth && m_currentHealth >= m_maxHealth * 0.75)
        {
            m_damagedAmount =1f;
        }


        if (m_currentHealth >= m_maxHealth *0.75 && m_currentHealth <m_maxHealth)
        {
            m_damagedAmount = 1.25f;
        }
        if (m_currentHealth >= m_maxHealth * 0.5 && m_currentHealth < m_maxHealth * 0.75)
        {
            m_damagedAmount = 1.5f;
        }
        if (m_currentHealth >= m_maxHealth * 0.25 && m_currentHealth < m_maxHealth * 0.5)
        {
            m_damagedAmount =  1.75f;
        }
        if (m_currentHealth >= 0  && m_currentHealth < m_maxHealth * 0.25)
        {
            m_damagedAmount =  2f;
        } // whole Update method is Lewis' code. It determines how much the enemy should stretch depending on how damaged it is. 
    }
    public override void TakeDamage( float damage )
    {
        base.TakeDamage( damage );
        StartCoroutine(damageFeedback()); // Lewis' code
        if( m_currentHealth <= 0 )
        {
            gameObject.GetComponent<EnemyBase>( ).Die( );
        }
    }


    IEnumerator damageFeedback()
    {
        m_spriteRenderer.color = Color.red;
        gameObject.transform.localScale = new Vector3(m_damagedAmount, 1f);
        yield return new WaitForSecondsRealtime(m_damagedTime);
        m_spriteRenderer.color = Color.white;
        gameObject.transform.localScale = new Vector3(1f, 1f);
    } // Whole Co-routine Lewis' code

}
