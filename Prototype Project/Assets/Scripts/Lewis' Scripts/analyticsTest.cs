using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class analyticsTest : MonoBehaviour
{
    // attach to player

    public int roomsCrossed = 0;
    public int enemiesKilled = 0;
    public string[] familyMembers = new string[] { "Greg", "Kate", "Adam", "Mia" };



    // Start is called before the first frame update
    void Start()
    {
        

}
    
    // Update is called once per frame
    void Update()
    {
      


     
        if (GameObject.Find("Player").GetComponent<HealthManager>().currentHealth == 0) // when player dies 
        {
            AnalyticsResult result = Analytics.CustomEvent("player died" + roomsCrossed); // send number of rooms player crossed out w/ player died tag
        Debug.Log("player died" + result); // tells us if the above was sent 
            Debug.Log(roomsCrossed);
        
        }
    }



    public void OnGameOver(ArrayList array)
    {
        Analytics.CustomEvent("Game Over", new Dictionary<string, object>
        {
            {  "rooms crossed", roomsCrossed},
            {"enemies killed", enemiesKilled},
            {"array", familyMembers }

        }
        
        
        
        );


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
