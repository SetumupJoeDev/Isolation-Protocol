using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controls functionality for the death screen UI
public class DeathUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField]
    private Button m_retryButton;

    [SerializeField]
    private Button m_surveyButton;

    // Start is called before the first frame update
    private void Start()
    {
        // Adds listeners to run respective methods when buttons are clicked
        m_retryButton.onClick.AddListener(Retry);

        m_surveyButton.onClick.AddListener(Survey);
    }

    // Reloads game from the start of level 1
    private void Retry()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Loads scene with link to feedback survey form for player to fill
    private void Survey()
    {
        SceneManager.LoadScene("Lewis' Test Scene");
    }
}
