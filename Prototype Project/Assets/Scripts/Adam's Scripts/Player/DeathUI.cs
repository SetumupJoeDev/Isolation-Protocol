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

    // Start is called before the first frame update
    private void Start()
    {
        // Adds Retry() to retryButton's on click event
        m_retryButton.onClick.AddListener(Retry);
    }

    // Reloads game from the start of level 1
    private void Retry()
    {
        SceneManager.LoadScene("Level 1");
    }
}
