using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// TODO -- Gun switch statements need to be updated with new weapons, add shop and drone feedback, add enemyCounter values, 
public class analyticsManager : MonoBehaviour
{
    public int basicMeleeKilled = 0;
    public int basicRangedKilled = 0;
    public int faceHuggerKilled = 0;
    public int LargeKilled = 0;
    public int sporeBomberKilled = 0;
    public int wallGrowthKilled = 0;
    public int parasiteEggKilled = 0;
    public int droidKilled = 0;
    public static int roomsCrossed = 0;
    public int bulletsHit = 0;



    public int boltBulletsShot = 0;
    public int boltHits = 0;
    public int subMachineGunBulletsShot = 0;
    public int subMachineGunBulletsHit = 0;

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
   
    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        packInformation();

        if (isPlaytester == true)
        {
            email.superiorMethod(this);
        }
        
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



    public void bulletCounter(string name) // ProjectileBase script passes values into this switch statemtent and calls it whenever it's enabled
    {
       
        switch (name)
        {
            case "BoltM4250":
                boltBulletsShot++;
                Debug.Log("bolt missed!" + boltBulletsShot);
                break;

            case "SubmachineGun":
                subMachineGunBulletsShot++;
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
                
        }

    }

    public void enemyCounterSwitch(string name)
    {
        switch (name)
        {
            case "BasicRangedEnemy(Clone)":
                Debug.Log(name);
                break;
        }
    }

    
    // Update is called once per frame
    void Update()  
    {

        currentTime += 1 * Time.deltaTime;
        timeInGame = currentTime.ToString();
        allBulletsShot = subMachineGunBulletsShot + boltBulletsShot;

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            onDeath();
            print("I sent!");
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

            // case gravy boy
            // case droid



        }
    }

   
   
}
