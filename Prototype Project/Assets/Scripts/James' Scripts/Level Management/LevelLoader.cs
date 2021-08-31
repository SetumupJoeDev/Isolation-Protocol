using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public void LoadNewLevel( string levelName )
    {
        //Loads the scene with the name passed in through arguments
        SceneManager.LoadScene( levelName );
    }

}
