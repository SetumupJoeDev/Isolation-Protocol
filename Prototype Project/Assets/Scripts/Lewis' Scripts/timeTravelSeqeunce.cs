using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeTravelSeqeunce : MonoBehaviour
{


   

    public List<enemyList> lists;

    public float  m_time;  // 1__________This float is a timekeeper. When the rewind time mechanic begins, this float will be counting backwards.
     // Enemy gameObjects with timeTravelEnabler attached will be continously watching this float and comparing it to a float called m_timeOfEnemyDeath.
     // If m_time is equal to or less than m_timeOfEnemyDeath, that gameObject begins it's rewind time mechanic

    public int count;
    public bool m_rewindTime = false; //2_____ This bool ensures that Rewind() does not begin prematurely
    


    public bool m_playerAlive; //3____ This bool also helps ensure that Rewind() and m_OnPlayerDeath(float) event does not begin prematurely

    public delegate bool m_checkEnemyCanRewind(float time);
    public static m_checkEnemyCanRewind m_checkEnemyRewind;

   
    // Start is called before the first frame update
    void Start()
    {


        m_checkEnemyRewind += EnemyCanBeginRewind;

        m_playerAlive = true;//4____  See 3
    }

    
    public bool EnemyCanBeginRewind(float enemyDeathTime)
    {
        if (enemyDeathTime >= m_time)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

  
    // Update is called once per frame
    void FixedUpdate()
    {
       
    



        if (Input.GetKeyDown(KeyCode.X))
        {
         
        m_playerAlive = false;

        }


        if (m_playerAlive == true) // ____5  While the player is alive, every frame..
        {
            m_time += Time.deltaTime; // ____6 The time will increase in line with deltaTime, so an accurate time is kept


        }
     //  if(count ==0 && m_playerAlive == false) //____7 When the player dies, the event below is triggered
     //   {



         //   timeTravelEnabler.m_OnPlayerDeath(m_time); //____8 If player dies, but some enemies survive, this event allows the surviving enemies to time travel. It also 
            // calls the method that begins time travel


          //  count++;//____9 This means that the event is only triggered once
          
 

      //  }


    //   if(m_rewindTime == true) //_____10 When the rewind begins, calls this method
      //  {
        //    RewindTime();
     //   }
        
      

       
    }


    public void RewindTime()
    {
        m_time -= Time.deltaTime;    //_____11 stop counting up, but count down to allow gameObjects with timeTravelEnabler to begin their rewind time mechanics at the correct time
        // See timeTravelEnabler line 43 for ____12
    }
}
