using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{

    public override void Activated( PlayerController playerController )
    {
        //Loads the 1st level
        SceneManager.LoadScene( "Level 1" );
    }

}
