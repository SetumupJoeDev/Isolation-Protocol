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
 public enemyCounter m_enemyCounter;
    int testValue = 5;

    
    void Start()
    {
        m_enemyCounter = GameObject.Find("easyName").GetComponent<enemyCounter>(); // finds the gameobject that stores important game data 



        //StartCoroutine(Post(testValue)); // Begins the process to send off the important game data 
    }



   public void writeEmail(int roomsCrossed, int basicMeleeKilled, int basicRangedKilled, int faceHuggerKilled, int LargeKilled, int sporeBomberKilled, int wallGrowthKilled, int droidKilled,int parasiteEggKilled) // arguments are the important data 
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.420162231", roomsCrossed); // finds the appropriate form for the data typed passed into this code
        form.AddField("entry.100712115", basicMeleeKilled);
        form.AddField("entry.4744759", basicRangedKilled);
        form.AddField("entry.1063026462", faceHuggerKilled);
        form.AddField("entry.1558735003", LargeKilled);
        form.AddField("entry.756172493", sporeBomberKilled);
        form.AddField("entry.1109825951", wallGrowthKilled);
        form.AddField("entry.1896346647", droidKilled);
        form.AddField("entry.1456948801",parasiteEggKilled);
        UnityWebRequest www = UnityWebRequest.Post(URL, form); // packs data up nicely 

        www.SendWebRequest(); // sends data off 

        ;
    }


    public void sendEmail(WWWForm form)
    {
       
    }

   



}