using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class textTest : MonoBehaviour
{
    void CreateText()
    {
        string path = @"C:\Users\Lewis.Reynolds.ADM\Documents\GitHub\Module-10-Project\Prototype Project\Assets\Scripts";

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
