using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCounter : MonoBehaviour
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
    public int boltBulletsHit = 0;
    public int subMachineGunBulletsShot = 0;

    public email email;
 

    // Start is called before the first frame update
    void Start()
    {
        email = GameObject.Find("easyName").GetComponent<email>();
        DontDestroyOnLoad(gameObject);
        
    }

    void onDeath() // Lewis' code. Called when the player dies, so to send off playtest data 
    {
        email.writeEmail(roomsCrossed, basicMeleeKilled, basicRangedKilled,faceHuggerKilled,LargeKilled,sporeBomberKilled,wallGrowthKilled,parasiteEggKilled,droidKilled);
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
                boltBulletsHit++;
                Debug.Log("bolt hit!" + boltBulletsHit);
                break;
        }

    }

    // Update is called once per frame
    void Update() // whole update method is a test to see if the email system works. 
    {
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
