using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    // Lewis' code

    SpriteRenderer m_spriteRenderer;
    public float m_damagedTime = 0.15f; 

    public float m_damagedAmount ;
    public float m_maxHealthFloat;
    public float m_currentHealthFloat;
    public int number = 5;

    public float m_timeAlive;
   

    public float[] hello;

    //End of Lewis's code.  

    public PlayerController m_playerController;

   public static List<enemyList> enemyLists = new List<enemyList>();
  
    
    public override void Start()
    {






  

        analyticsEventManager.analytics.onEnemySpawn(gameObject.name);
        //Sets the spriteRenderer to be that attached to the gameObject
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    } 


 


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(1);
        }



        //Lewis' code
       
        m_timeAlive += Time.deltaTime;

        m_currentHealthFloat = (float)m_currentHealth;
    
        m_damagedAmount = m_maxHealth / m_currentHealthFloat;
        
        //End of Lewis' code

    }
    public override void TakeDamage( int damage )
    {
       

        base.TakeDamage( damage );

        StartCoroutine(damageFeedback()); // Lewis' code

        //If the enemy's health reaches 0, they die and an event is called for the analytics manager to register an enemy death
        if( m_currentHealth <= 0 )
        {
            if ( gameObject.GetComponent<EnemyBase>( ) != null )
            {

             
                timeTravelEnabler.m_OnEnemyDeath(); // This calls an event on timeTravelEnabler, when the enemy dies, to prepare the enemy for the time rewind mechanic, 
                // go to line 50 in timeTravelEnabler script
                

              //  gameObject.GetComponent<EnemyBase>().Die(); // for the time being, we don't want to destroy the gameObject due to the time rewind mechanic


                analyticsEventManager.analytics.onEnemyDeath( gameObject.name ); 

               

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
