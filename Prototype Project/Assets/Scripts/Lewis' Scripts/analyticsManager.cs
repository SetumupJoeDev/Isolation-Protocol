using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// TODO -- Gun switch statements need to be updated with new weapons, add shop and drone feedback, add enemyCounter values, 
public class analyticsManager : MonoBehaviour
{
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
    public static int roomsCrossed = 0;
    public int bulletsHit = 0;


    public int basicRangedSpawned;
    public int basicMeleeSpawned;
    public int faceHuggerSpawned;
    public int LargeSpawned;
    public int sporeBomberSpawned;
    public int wallGrowthSpawned;
    public int parasiteEggSpawned;
    public int gravyDroidSpawned;
    public int stunDroidSpawned;


    public int basicRangedAttacked;
    public int basicMeleeAttacked;
    public int faceHuggerAttacked;
    public int LargeAttacked;
    public int sporeBomberAttacked;
    public int wallGrowthAttacked;
    public int parasiteEggAttacked;
    public int droidAttacked;
    public int stunDroidAttacked;





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
   
    public int allBulletsShot;

    public float currentTime = 0f;
    public string timeInGame;

    public int ciggiesTotal;
    public int fabricatorFuelTotal;
    public int ciggiesCurrent;
    public int fabricatorFuelCurrent;

    public email email;

    public CurrencyManager currencyManager; 

    public bool isPlaytester = false;

    public string activeScene;


    List<averageClass> list = new List<averageClass>();



    public void OnEnable()
    {
        analyticsEventManager.analytics.bulletShootIncrement += countBolt;

        analyticsEventManager.analytics.bulletHitIncrement += bulletHitCounter;

        analyticsEventManager.analytics.enemyDeathIncrement += incrementKilledNewEnemy;

        analyticsEventManager.analytics.countEnemyIncrement += enemyCounterSwitch;

        analyticsEventManager.analytics.onEnemyAttackIncrement += enemyAttack;

        analyticsEventManager.analytics.onBuyItemIncrement += itemBuy;


        // Put all of the events and their corresponding methods here 
    }
    public void itemBuy(string str)
    {
        Debug.Log(str);
    }


    public void enemyAttack(string str)
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
    }

    public void bulletHitCounter(string name)
    // ProjectileBase script passes values into this switch statemtent and calls it whenever it's collides with an enemy
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

    }

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
    }

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
    }







    // Start is called before the first frame update
    void Start()
    {



     



        DontDestroyOnLoad(gameObject);
        if (GameObject.Find("easyName").GetComponent<email>() != null)
        {
            email = GameObject.Find("easyName").GetComponent<email>();
           
        }
        if (GameObject.Find("Player").GetComponent<CurrencyManager>() != null)
        {
            currencyManager = GameObject.Find("Player").GetComponent<CurrencyManager>();
        }

        SceneManager.sceneLoaded += onSceneLoaded; 
    }

    private void Hello_OnGrappleTaskStart()
    {
        throw new NotImplementedException();
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        packInformation();

        if (isPlaytester == true)
        {
            email.superiorMethod(this);
        }
        
    }


  
   
    public void AverageNumber()
    {
        // List(enemyEnum + complete list of times that enemy died)
        // Sum list, divide by number in list

    }

    public void roomCount()
    {
     
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    
        isPlaytester = true;
        playTestEnable.m_isPlaytester = true;

        // call countenemy();

    }


    public void packInformation()
    {
        ciggiesCurrent = currencyManager.m_cigarettePacksCount;
        ciggiesTotal = currencyManager.m_TotalCigarettePacksCount;
        fabricatorFuelCurrent = currencyManager.m_fabricatorFuelCount;
        fabricatorFuelTotal = currencyManager.m_totalFabricatorFuelCount;
        activeScene = SceneManager.GetActiveScene().name;
    }


public    void onDeath() // Lewis' code. Called when the player dies, so to send off playtest data 
    {
       


        if (isPlaytester == true)
        {
            packInformation();

            email.superiorMethod(this);
            Debug.Log("death works");
        }
    }

    public void itemBroughtCounter (string name)
    {
        // fabricatorStoreBase.buyselecteditem() runs this switch statement, passes in m_selectedItemIndex and increments this item brought
    }




    

    

    
    // Update is called once per frame
    void Update()  
    {
        //   gameEvents.hello.runGoodbye();
   

        currentTime += 1 * Time.deltaTime;
        timeInGame = currentTime.ToString();
        allBulletsShot = subMachineGunBulletsShot + boltBulletsShot;

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            onDeath();
            print("I sent!");
        }
    }


     
  
}
