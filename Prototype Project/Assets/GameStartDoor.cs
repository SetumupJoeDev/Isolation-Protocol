using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartDoor : InteractableObject
{
    [Tooltip("Toggles hidden player objects to show in level scene")]
    [SerializeField]
    private GameObject[] m_togglePlayerObjects;

    public override void Activated( PlayerController playerController )
    {
        // Loads the 1st level
        SceneManager.LoadScene( "Level 1" );

        for (int i = 0; i < m_togglePlayerObjects.Length; i++)
        {
            m_togglePlayerObjects[i].SetActive(true);
        }
    }
}
