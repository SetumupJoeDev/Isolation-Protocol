using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public void LoadNewLevel( string levelName )
    {
        SceneManager.LoadScene( levelName );
    }

}
