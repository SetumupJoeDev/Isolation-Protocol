using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class textTest : MonoBehaviour
{
    void CreateText()
    {
        string path = Application.dataPath + "/Scripts/Log.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");

        }

        string content = "hello";
        File.AppendAllText(path, content);
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
