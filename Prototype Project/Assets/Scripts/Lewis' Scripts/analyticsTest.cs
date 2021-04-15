using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class analyticsTest : MonoBehaviour
{
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {

        GameObject weapon = GameObject.Find("TestWeapon(1)");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // If the event we want to track happens, do this
        {
            AnalyticsResult anayl = Analytics.CustomEvent("gameOver" +3) ;
            Debug.Log("analy:" + anayl);
        }
    }
}
