using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Ship Hub");
        Time.timeScale = 1;
    }

    public void Tutorial( )
    {
        SceneManager.LoadScene( "TutorialScene" );
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void LinkToSurvey()
    {
        Application.OpenURL("https://forms.gle/d38QM6iLT82SNn3A7");
    }
}
