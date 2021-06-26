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
    public static int counter = 0;


 

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        
    }

    void CountEnemies()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       // print(counter);
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
