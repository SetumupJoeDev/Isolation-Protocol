using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    protected int m_currentLevelNumber;
    protected void Start()
    {
        m_currentLevelNumber = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<LevelController>().m_levelNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level " + (m_currentLevelNumber + 1));
        }
    }
}
