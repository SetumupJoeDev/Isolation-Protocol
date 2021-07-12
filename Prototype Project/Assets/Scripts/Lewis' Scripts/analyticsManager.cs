using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
     
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
         
    
        isPlaytester = true;
        playTestEnable.m_isPlaytester = true;


    }



public    void onDeath() // Lewis' code. Called when the player dies, so to send off playtest data 
    {
        ciggiesCurrent = currencyManager.m_cigarettePacksCount;
        ciggiesTotal = currencyManager.m_TotalCigarettePacksCount;
        fabricatorFuelCurrent = currencyManager.m_fabricatorFuelCount;
        fabricatorFuelTotal = currencyManager.m_totalFabricatorFuelCount;
        if (isPlaytester == true)
        {
            email.superiorMethod(this);
            Debug.Log("death works");
        }
    }

    public void bulletCounter(int number, string name)
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

    public void bulletHitCounter(string name, string enemyName)
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

    // Update is called once per frame
    void Update() // whole update method is a test to see if the email system works. 
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
