using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{

    public override void Activated( )
    {
        //Loads the next level by incrementing the current level's build index
        SceneManager.LoadScene( SceneManager.GetActiveScene( ).buildIndex + 1 );
    }

}
