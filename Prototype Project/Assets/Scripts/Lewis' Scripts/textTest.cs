using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class textTest : MonoBehaviour
{
    private int dataInt;
    public string dataString;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScdSRxOY_GA7c0ip5GSU4cranwToRn3b3DEGnoK44U02gSW8A/formResponse";

    void Start()
    {
        Send();
        CreateText();
    }

    IEnumerator Post(string dataString1)
    {
        print("I posted");
        WWWForm form = new WWWForm();
        form.AddField("entry.63820867", dataString1);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }
    public void Send()
    {
        print("I sent");
        dataInt = gameObject.GetComponent<testScript>().goodbye;
        dataString = string.Empty + dataInt;
        StartCoroutine(Post(dataString));
    }







    void CreateText()
    {
        string path = Application.dataPath + "/Scripts/Log.csv";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");

        }

        string content = "hello";
        File.AppendAllText(path, content);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
