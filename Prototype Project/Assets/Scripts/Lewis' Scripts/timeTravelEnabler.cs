using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeTravelEnabler : MonoBehaviour
{
    public GameObject player;

    public EnemyHealthManager healthManager;
    public Component[] comps;
    public bool m_isDead;

    public bool M_isRewinding;
    public int count = 0;

    public float m_timeOfEnemyAlive = 0f;
    public float m_timeOfEnemyDeath;

    List<Vector3> Positions;
    List<ProjectileList> projectileLists;



    public delegate void m_EnemyDeath();
    public static m_EnemyDeath m_OnEnemyDeath;

    public delegate void m_PlayerDeath(float time);
    public static m_PlayerDeath m_OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {

        projectileLists = new List<ProjectileList>();

        count = 0;

        Positions = new List<Vector3>();

        healthManager =  gameObject.GetComponent<EnemyHealthManager>();

        m_OnEnemyDeath += PrepareEnemyForRewind; // Go to EnemyHealthManager Line 86 for explaination
        m_OnPlayerDeath += OnPlayerDeathButEnemySurvival; // Line 115
        m_OnPlayerDeath += Begin; //Line 123
        //_____12 The Functions needed for the rewind mechanic [OnPlayerDeathButEnemySurival && Begin] are associated to m_OnPlayerDeath Event. Go to Line 106 for ____13


    }

    public void PrepareEnemyForRewind()  //This disables the Sprite renderer, and sets the time of the enemy's death, used to trigger TrueRewind() at the right time, and disables all
        //components and sets the bool to true to stop the counter. This method is triggered in EnemyHealthManager Line 86 (When the enemy dies)
        // This method effectively kills the enemy, but does not destory the gameObject so it can be used in the rewind time mechanic. I will add a function or CoRoutine at some point
        // so that the enemy is not clogging up memory, but only a short backlog of enemies are stored ready for the rewind time mechanic
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        m_timeOfEnemyDeath = m_timeOfEnemyAlive;
        for (int i = 0; i <= comps.Length; i++)
        {
            
            Component compToBeDestroyed = comps[i];
         
            if (compToBeDestroyed != null)
            {
                Destroy(compToBeDestroyed);

            }

            m_isDead = true;

        }
    }







    // Update is called once per frame
    void Update()
    {

       

        
   

        




        }


    private void FixedUpdate()
    {
        
            TrueRewind(m_timeOfEnemyDeath); //___ 14 Calls the Rewind Method every Physics Update
        
     
        if(m_isDead == false) //While the enemy is still alive, the Record() method is called every physics update. The Record() method 
              // Stores in a List the enemy's transform and other actions, allowing for an accurate rewind, it also keeps a timer updated
        {
            Record();
        }
    }
    //____13 See below for how these methods allow the Rewind time mechanic to start      Go to Line 97 and Find TrueRewind in the FixedUpdate method for ____14
    #region Methods for rewind time mechanic
    public void OnPlayerDeathButEnemySurvival(float m_timeOfPlayerDeath) //Method called at timeTravelSequence Line 69 (eeeeeeey) when the player dies
        // When player dies, this method assigns enemyDeath to be equal to player death time
       // The rewind method uses enemyDeath, despite the enemy not dying, this means the enemy can rewind correctly. 
    {
       m_timeOfEnemyDeath = m_timeOfPlayerDeath; // Explained above
        m_isDead = true; //this stops the Record() function and the timer on the enemy for memory purposes. 
    }


    public void Begin(float time) //This method is called at timeTravelSequence Line 69 (eeeey) when the player dies
     // this method allows the rewind time mechanic to begin (the delegate method this event is assigned takes a float arguement,  this method has float argue so it can be assigned to the event)
    {
      
       M_isRewinding = true; //This allows TrueRewind() to begin Rewinding time every fixedUpdate

    }
    #endregion 
    public void Record() // This method is responsible for updating the 2 Lists every FixedUpdate frame.
    {
        m_timeOfEnemyAlive += Time.deltaTime; //Updates the float to reflect time
        Debug.Log("I am recording");
        Positions.Insert(0, transform.position);  //Stores the current transform.position at the first index of this list.
        //Record what was shot off, where and when
        //Receive shooting data from ranged enemy via Methid(data needed for list creation) and put that data into a list, including time when this was called
    }

    public void TrueRewind(float m_enemyDeathTime)
    {
        if (M_isRewinding == true && m_enemyDeathTime >= player.GetComponent<timeTravelSeqeunce>().m_time) 
            //___15 This method is called every Fixedupdate, but will only set off if we are currently Rewinding and enemyDeathTime is equal to the timeKeeper's time
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true; //___16 This sprite is now visible, so if it had been killed, it can now be seen
            Debug.Log("I'm rewinding");
            transform.position = Positions[0]; // ____17 Every FixedUpdate, the current transform.position will be changed to the first stored index's transform.position 
            // Go to Record() method at Line 125 to see how this works
            Positions.RemoveAt(0); //____18 Deletes the first index every FixedUpdate, so that the next index becomes the first, allowing this method to continously rewind
        }
        //Replay what was shot off, at when and at where
       //for (i==0; while i >= ListOfProjectiles[].length,++)
       //if (ListOfProjectiles[i].timeOfDeath == player.GetComponent<timeTravelSequnece>().m_time){
       //   InstanitateGameObject(ListOfProjectiles[i].transformPositionOfDeath etc.)
       //   Set the movement of this gameObject to be ListOfProjectiles[i].speed
       //   Move From currentTransform.Position -> ListOfProjectiles[i].transformPositionOfStart
       //   If InstanitateGameObject.transform.position == ListOfProjectiles[i].transformPositionOfStart{
       //   Destroy(InstaniatedGameObject)
       //     }
       //      }

    }
}

