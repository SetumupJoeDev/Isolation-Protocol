using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject m_areYouSurePanel;

    public void ShipHub()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetActiveScene());
        }
        
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

    public void ClearSaveData()
    {
        SaveSystem.DeletePlayerData();
        SaveSystem.DeleteFabricatorData();
    }

    public void AreYouSure()
    {
        m_areYouSurePanel.SetActive(true);
    }

    public void Cancel()
    {
        m_areYouSurePanel.SetActive(false);
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
        SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetActiveScene());
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void LinkToSurvey()
    {
        Application.OpenURL("https://forms.gle/d38QM6iLT82SNn3A7");
    }
}
