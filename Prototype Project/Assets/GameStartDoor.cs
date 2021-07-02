using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{

    public override void Activated( )
    {
        //Loads the 1st level
        SceneManager.LoadScene( "Player Name Screen" );
    }

}
