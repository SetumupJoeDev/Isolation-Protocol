using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class scanPath : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scanMap());
    
      
    }



   public  IEnumerator scanMap()
    {
        Debug.Log("hello");

        yield return new WaitForSecondsRealtime(5f);
        Debug.Log("hello");
        AstarPath.active.Scan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
