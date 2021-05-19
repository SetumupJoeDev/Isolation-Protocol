using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;

    // Start is called before the first frame update
    void Start()
    {
        Button button = retryButton.GetComponent<Button>();
        button.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        SceneManager.LoadScene("Level 1");
    }
}
