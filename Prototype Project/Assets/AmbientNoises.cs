using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoises : MonoBehaviour
{

    public AudioClip[] m_ambientNoises;
    public Vector3 m_location;
    public float m_time;
    public float m_Index = 5f;
    public float m_MinTimeBetweenSFX;
    public float m_MaxTimeBetweenSFX;
    public int m_randSFX;
    public float m_xPositionForSound;
    public float m_yPositionForSound;
    public float m_zPositionForSound;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime; // Updates time as a value to reflect real game time


        

        if(m_time >= m_Index) 
        {
            m_Index = Random.Range(m_MinTimeBetweenSFX, m_MaxTimeBetweenSFX); //Selects a random time to reset the sfx loop
            m_randSFX = Random.Range(1, 9); // Selects a random sound to be played
            m_location.Set(m_xPositionForSound, m_yPositionForSound, m_zPositionForSound); //sets location to be whatever is assigned in Unity
            AudioSource.PlayClipAtPoint(m_ambientNoises[m_randSFX], m_location);
            m_time = 0f; //resets time, 
        }

        
    }
}
