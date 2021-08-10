using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// TODO -- Gun switch statements need to be updated with new weapons, add shop and drone feedback, add enemyCounter values, 
public class analyticsManager : MonoBehaviour
{

    #region Count of killed enemies 
    [Header("Count of killed enemies")]
    public int basicMeleeKilled = 0;
    public int basicMeleeKilledFirst;
    public int basicRangedKilled = 0;
    public int basicRangedKilledFirst;
    public int faceHuggerKilled = 0;
    public int faceHuggerKilledFirst;
    public int LargeKilled = 0;
    public int LargeKilledFirst;
    public int sporeBomberKilled = 0;
    public int sporeBomberKilledFirst;
    public int wallGrowthKilled = 0;
    public int wallGrowthKilledFirst;
    public int parasiteEggKilled = 0;
    public int parasiteEggKilledFirst;
    public int droidKilled = 0;
    public int droidKilledFirst;
    public int stunDroidKilled;
    [Space]
    #endregion


    public static int roomsCrossed = 0;
    public int bulletsHit = 0;

    #region count of Enemies Spawned 
    [Header("Count of Enemies Spawned")]
    public int basicRangedSpawned;
    public int basicMeleeSpawned;
    public int faceHuggerSpawned;
    public int LargeSpawned;
    public int sporeBomberSpawned;
    public int wallGrowthSpawned;
    public int parasiteEggSpawned;
    public int gravyDroidSpawned;
    public int stunDroidSpawned;
    [Space]
    #endregion

    #region Count of enemies attacked
    [Header("Count of enemies attacked")]
    public int basicRangedAttacked;
    public int basicMeleeAttacked;
    public int faceHuggerAttacked;
    public int LargeAttacked;
    public int sporeBomberAttacked;
    public int wallGrowthAttacked;
    public int parasiteEggAttacked;
    public int droidAttacked;
    public int stunDroidAttacked;
    [Space]
    #endregion


    #region bullets hit and Shot
    [Header("Bullets hit and shot counter")]
    public int boltBulletsShot = 0;
    public int boltHits = 0;
    public int subMachineGunBulletsShot = 0;
    public int subMachineGunBulletsHit = 0;
    public int burstRifleShot;
    public int burstRifleHit;
    public int DMRshot;
    public int DMRHit;
    public int minigunShot;
    public int minigunHit;
    public int SawnOffShotgunShot;
    public int SawnOffShotgunHit;
    public int SniperRifleShot;
    public int SniperRifleHit;
    public int TacticalRifleShot;
    public int TacticalRifleHit;
    public int TacticalShotgunShot;
    public int TacticalShotgunHit;
   
    [Space]
    #endregion

    #region miscellanous 
    [Header("Miscellanous")]
    public float currentTime = 0f;
    public string timeInGame;

    public int ciggiesTotal;
    public int fabricatorFuelTotal;
    public int ciggiesCurrent;
    public int fabricatorFuelCurrent;

 

    

    public bool isPlaytester = false;

    public string activeScene;


    public email email;

    #endregion


    public CurrencyManager CurrencyManager;

    public void OnEnable()
    {
        

        analyticsEventManager.analytics.bulletShootIncrement += countBolt;

        analyticsEventManager.analytics.bulletHitIncrement += bulletHitCounter;

        analyticsEventManager.analytics.enemyDeathIncrement += incrementKilledNewEnemy;

        analyticsEventManager.analytics.countEnemyIncrement += enemyCounterSwitch;

        analyticsEventManager.analytics.onEnemyAttackIncrement += enemyAttack;

        analyticsEventManager.analytics.onBuyItemIncrement += itemBuy;

        analyticsEventManager.analytics.onPlayerDeath += onDeath;

        analyticsEventManager.analytics.CurrencyIncrement += packInfo;

        // Put all of the events and their corresponding methods here 
    }
    public void itemBuy(string str)
    {
     //   Switch statement that counts how many of a certain type of item was created
    }


    public void enemyAttack(string str) // counts how many times the enemy attacks
    {
       switch (str)
        {
            case "BasicEnemyMelee(Clone)":
                basicMeleeAttacked++;
                break;

            case "SporeBomber(Clone)":
                sporeBomberAttacked++;
                break;

            case "SporeHealer(Clone)":
                wallGrowthAttacked++;
                break;

            case "ParasiteEgg(Clone)":
                parasiteEggAttacked++;
                break;

            case "MutoSlug(Clone)":

                faceHuggerAttacked++;

                break;

            case "BasicRangedEnemy(Clone)":
                basicRangedAttacked++;
                break;

            case "Amalgam(Clone)":
                LargeAttacked++;
                break;

            case "GravyDroid(Clone)":
                droidAttacked++;
                break;

            case "GuardDroid(Clone)":
                stunDroidAttacked++;
                break;

        }

    }

    public void countBolt(string str)
    {
        switch (str)
        {
            case "BoltM4250":
                boltBulletsShot++;
                //     list.Insert(1, new averageClass(boltBulletsShot));

                break;

            case "SubmachineGun":
                subMachineGunBulletsShot++;
                break;

            case "BurstRifle":
                burstRifleShot++;
                break;

            case "DMR":
                DMRshot++;
                break;

            case "Minigun":
                minigunShot++;
                break;

            case "Sawn off shotgun":
                SawnOffShotgunShot++;
                break;

            case "SniperRifle":
                SniperRifleShot++;
                break;

            case "Tactical rifle":
                TacticalRifleShot++;
                break;

            case "Tactical Shotgun":
                TacticalShotgunShot++;
                break;
        }
    } // ProjectileBase Script passes values into this switch statement whenever it is enabled, counting how many bullets were spawned from what gun 

    public void bulletHitCounter(string name)
    {
        switch (name)
        {
            case "BoltM4250":
                boltHits++;
                Debug.Log("bolt hit!" + boltHits);
                break;

            case "SubmachineGun":
                subMachineGunBulletsHit++;
                break;

            case "BurstRifle":
                burstRifleHit++;
                break;

            case "DMR":
                DMRHit++;
                break;

            case "Minigun":
                minigunHit++;
                break;

            case "Sawn off shotgun":
                SawnOffShotgunHit++;
                break;

            case "SniperRifle":
                SniperRifleHit++;
                break;

            case "Tactical rifle":
                TacticalRifleHit++;
                break;

            case "Tactical Shotgun":
                TacticalShotgunHit++;
                break;
        }

    }    // ProjectileBase script passes values into this switch statemtent and calls it whenever it's collides with an enemy


    public void enemyCounterSwitch(string name)
    {
        switch (name)
        {
            case "BasicRangedEnemy(Clone)":
                basicRangedSpawned++;
                break;
            case "BasicEnemyMelee(Clone)":
                basicMeleeSpawned++;
                break;
            case "GravyDroid(Clone)":
                gravyDroidSpawned++;
                break;
            case "MutoSlug(Clone)":
                faceHuggerSpawned++;
                break;
            case "ParasiteEgg(Clone)":
                parasiteEggSpawned++;
                break;

            case "SporeBomber(Clone)":
                sporeBomberSpawned++;
                break;

            case "Amalgam(Clone)":
                LargeSpawned++;
                break;

            case "SporeHealer(Clone)":
                wallGrowthSpawned++;
                break;

            case "GuardDroid(Clone)":
                stunDroidSpawned++;
                break;
        }
    } // EnemyHealthManager onStart() passes a name into this switch statement which counts how many enemies have spawned 

    public void incrementKilledNewEnemy(string enemyName)
    {
        switch (enemyName)
        {
            case "BasicEnemyMelee(Clone)":
                basicMeleeKilled++;
                break;

            case "SporeBomber(Clone)":
                sporeBomberKilled++;
                break;

            case "SporeHealer(Clone)":
                wallGrowthKilled++;
                break;

            case "ParasiteEgg(Clone)":
                parasiteEggKilled++;
                break;

            case "MutoSlug(Clone)":

                faceHuggerKilled++;

                break;

            case "BasicRangedEnemy(Clone)":
                basicRangedKilled++;
                break;

            case "Amalgam(Clone)":
                LargeKilled++;
                break;

            case "GravyDroid(Clone)":
                droidKilled++;
                break;

            case "GuardDroid(Clone)":
                stunDroidKilled++;
                break;



        }
    } // EnemyBase passes the name of it's gameObject before it dies into this switch statement which counts how many enemies have died



    



    // Start is called before the first frame update
    void Start()
    {




        

        DontDestroyOnLoad(gameObject);
   
      

        SceneManager.sceneLoaded += onSceneLoaded; 
    }

  

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) // whenver a new scene is loaded 
    {

     //   packInformation(); // keeps a track of the currency by calling a method which counts it 

       
            analyticsEventManager.analytics?.passAnalytics(this); // When the player loads into a new scene, it sends data from this script into Email 
        
        
    }


  
   


    public void OnTriggerEnter2D(Collider2D collision) // this script's gameObject has a renderer and a collider attached, if the player runs into it, it indicates they're a playtester 
    {
        
    
        isPlaytester = true;
        playTestEnable.m_isPlaytester = true;
        

    }


     


    
    public void packInfo(CurrencyManager currency)
    {
        ciggiesCurrent = currency.m_cigarettePacksCount;
        ciggiesTotal = currency.m_fabricatorFuelCount ;
        fabricatorFuelCurrent = currency.m_totalFabricatorFuelCount;
        fabricatorFuelTotal = currency.m_TotalCigarettePacksCount;
        Debug.Log(currency.m_TotalCigarettePacksCount);
        activeScene = SceneManager.GetActiveScene().name;

    }

public    void onDeath() // Lewis' code. Called when the player dies, so to send off playtest data 
    {

    

        if (isPlaytester == true)
        {
          
           // analyticsEventManager.analytics?.passAnalytics(this); // When the player dies, it sends data from this script into Email 
            email.superiorMethod(this);
            Debug.Log("death works");
        }
    }

   



    

    

    
    
    void Update()  
    {

       
        currentTime += 1 * Time.deltaTime;
        timeInGame = currentTime.ToString();



        if (Input.GetKeyDown(KeyCode.H))
        {
            email.superiorMethod(this);
            Debug.Log("I run");
            //analyticsEventManager.analytics?.passAnalytics(this);
            Debug.Log("I ran!");
        }
    }


     
  
}
