using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class analyticsTest : MonoBehaviour
{
    // attach to player

    public bool isActive;
    public int roomsCrossed = 0;

    // Start is called before the first frame update
    void Start()
    {
        

}
    
    // Update is called once per frame
    void Update()
    {
      


     
        if (GameObject.Find("Player").GetComponent<HealthManager>().currentHealth == 0 && isActive == false)   // when player dies 
        {
            

            isActive = true;
           /* AnalyticsResult result = Analytics.CustomEvent("player died" + roomsCrossed); // send number of rooms player crossed out w/ player died tag
        Debug.Log("player died" + result); // tells us if the above was sent 
            Debug.Log(roomsCrossed); */
        
        }
    }



    public void OnGameOver()
    {



        int number = 10;
        int enemiesKilled = 15;
      //  string[] familyMembers = new string[] { "Greg", "Kate", "Adam", "Mia" };
        AnalyticsResult result = Analytics.CustomEvent("Game Over", new Dictionary<string, object>
        {
            {  "rooms crossed", number},
            {"enemies killed", enemiesKilled},
         //   {"array", familyMembers }

        }
        
        
        
        
        );
        Debug.Log("Game over" + result);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.layer == 12) // checks for doorcollider
            {

                collision.gameObject.GetComponent<AnalyticsEventTracker>().enabled = true; // creates an event on that gameobject, can send off data here


                roomsCrossed++; // adds 1 onto rooms crossed
            }
        }
    }


    // Oncollision function, on every door, place a collider. When player hits collider, levelscrossed++
}
