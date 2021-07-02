using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playTestEnable : MonoBehaviour
{

    TextMeshProUGUI text;
    public Text m_playerNameScreenText;
    public Text m_finalDisplayPlayerName;
    public static bool m_isPlaytester; 
    public static string m_playerName = "hello";
    public bool m_isActivated; 
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
         
        text = gameObject.GetComponent<TextMeshProUGUI>();
         m_finalDisplayPlayerName = GameObject.Find("Feedback scene Canvas").GetComponent<Text>(); 
        m_finalDisplayPlayerName.text = "your player name is " + m_playerName; // Displays the player name to them, so they remember for the feedback form

    }

    // Update is called once per frame
    void Update()
    {

        if (m_isPlaytester == true && m_isActivated == false) // if the player is a playtester and has confirmed this, runs once. 
        {
            m_isActivated = true;
            if (GameObject.Find("Text TMP") != null) {
              
                text.text = "Confirmed!"; // informs player that they have confirmed they are a playtester
                
         }       
        }
    }
    public void NextLevel()
    {
        if (m_playerName != null)
        {
            SceneManager.LoadScene("Level 1"); // at player name enter scene, lets the player go onto the next level 
        }
        else
        {
            m_playerNameScreenText.text = "Please press *Enter* before continuing";
        }
    }
public    void playerName(string name)
    {
        m_playerName = name;
        Debug.Log(m_playerName);
    }
}
