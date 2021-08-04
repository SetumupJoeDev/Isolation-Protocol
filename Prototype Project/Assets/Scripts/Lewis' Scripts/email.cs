using System.Collections;
using System.Net;
using System.Net.Mail;
using UnityEngine.Networking;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class email : MonoBehaviour
{

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScryss7QVf9Vv5Ab45uzcHDfjKaihY-efWtNNcIrtUwpbmI6A/formResponse";
 public analyticsManager m_enemyCounter;
    public playTestEnable m_playTestEnable;
    int testValue = 5;

    float currentTime = 0;
    string timeInGame;

    
    void Start()
    {
        if (GameObject.Find("easyName").GetComponent<analyticsManager>() != null)
        {

            m_enemyCounter = GameObject.Find("easyName").GetComponent<analyticsManager>(); // finds the gameobject that stores important game data 
        }

        if (GameObject.Find("playTesterName").GetComponent<playTestEnable>() != null)
        {
            m_playTestEnable = GameObject.Find("playTesterName").GetComponent<playTestEnable>();
        }

        //StartCoroutine(Post(testValue)); // Begins the process to send off the important game data 
    }

     void Update()
    {
       
    }

    //this script collects data from manager and sends it off with the method below. 


    public void superiorMethod(analyticsManager analyticData)
    {
      
        WWWForm form = new WWWForm();
        Debug.Log(analyticData.basicMeleeKilled);
        form.AddField("entry.420162231", analyticData.roomsCrossed); // finds the appropriate form for the data typed passed into this code
        form.AddField("entry.100712115", analyticData.basicMeleeKilled);
        form.AddField("entry.4744759", analyticData.basicRangedKilled);
        form.AddField("entry.1063026462", analyticData.faceHuggerKilled);
        form.AddField("entry.1558735003", analyticData.LargeKilled);
        form.AddField("entry.756172493", analyticData.sporeBomberKilled);
        form.AddField("entry.1109825951", analyticData.wallGrowthKilled);
        form.AddField("entry.1896346647", analyticData.droidKilled);
        form.AddField("entry.1456948801", analyticData.parasiteEggKilled);
        form.AddField("entry.426456186", analyticData.timeInGame);
        form.AddField("entry.1975649807", analyticData.allBulletsShot);
        form.AddField("entry.1218885744", analyticData.boltHits);
        form.AddField("entry.1493791387", analyticData.subMachineGunBulletsHit);
        form.AddField("entry.1928014227", analyticData.boltBulletsShot);
        form.AddField("entry.978007937", analyticData.subMachineGunBulletsShot);
        form.AddField("entry.1613226958",playTestEnable.m_playerName);
        form.AddField("entry.1240865199", analyticData.ciggiesTotal);
        form.AddField("entry.1902725297", analyticData.ciggiesCurrent);
        form.AddField("entry.1360343214", analyticData.fabricatorFuelTotal);
        form.AddField("entry.441036039", analyticData.fabricatorFuelCurrent);
        form.AddField("entry.538467984", analyticData.activeScene);


       

        UnityWebRequest www = UnityWebRequest.Post(URL, form); // packs data up nicely 
        www.SendWebRequest();

        Debug.Log("I worked!");
       // send(form, www);

         // sends data off 
    }



   


   



}