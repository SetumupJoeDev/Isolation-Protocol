using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    SpriteRenderer m_spriteRenderer;// Lewis' code
    public float m_damagedTime = 0.15f; // Lewis's code.

    public float m_damagedAmount ;
    public float m_maxHealthFloat;
    public float m_currentHealthFloat;
    public int number =5;

    public analyticsManager m_enemyCounter; // Lewis' and James' code
    public PlayerController m_playerController;


    public override void Start()
    {
       

        try
        {
            m_enemyCounter = GameObject.Find( "easyName" ).GetComponent<analyticsManager>( );
            m_enemyCounter.enemyCounterSwitch(gameObject.name);

        }
        catch (NullReferenceException e )
        {
            Debug.LogWarning( "Couldn't find an Analytics Manager in the scene." );
        }
        //Sets the spriteRenderer to be that attached to the gameObject
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    } 


    private void Update()
    {
        //Debug.Log(m_damagedAmount);
        //Debug.Log(m_currentHealthFloat + "current health float");
        //Debug.Log(m_maxHealthFloat + "max health float");

        m_currentHealthFloat = (float)m_currentHealth;
    
        m_damagedAmount = m_maxHealth / m_currentHealthFloat;
        


 
    }
    public override void TakeDamage( int damage )
    {
        if ( m_enemyCounter != null )
        {
            m_enemyCounter.bulletsHit++;
        }
       // m_playerController.m_currentWeapon.name
        // pass in the gameobject which is active 

        base.TakeDamage( damage );

        StartCoroutine(damageFeedback()); // Lewis' code

        //If the enemy's health reaches 0, they die
        if( m_currentHealth <= 0 )
        {
            if ( gameObject.GetComponent<EnemyBase>( ) != null )
            {
                if(m_enemyCounter != null) // important thing about code, needs to be able to run independetly 
                {
                    m_enemyCounter.incrementKilledNewEnemy(gameObject.name);

                }
                gameObject.GetComponent<EnemyBase>( ).Die( );
            }
            else
            {
                Destroy( gameObject );
            }
        }
    }

    
    IEnumerator damageFeedback()
    {
        m_spriteRenderer.color = Color.red;
        m_spriteRenderer.size = new Vector2(m_damagedAmount, 1f);
        //gameObject.transform.localScale = new Vector3(m_damagedAmount, 1f);
        yield return new WaitForSecondsRealtime(m_damagedTime);
        m_spriteRenderer.size = new Vector2(1f, 1f);
        m_spriteRenderer.color = Color.white;
     //   gameObject.transform.localScale = new Vector3(1f, 1f);
    } // Whole Co-routine Lewis' code

}
