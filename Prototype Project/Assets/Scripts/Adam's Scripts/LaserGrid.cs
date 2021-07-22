using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_lasers; 
    public float m_timeOn;
    public float m_timeOff;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(On());
    }
    
    private IEnumerator On()
    {
        for (int i = 0; i < m_lasers.Length; i++)
        {
            m_lasers[i].SetActive(true);
        }

        yield return new WaitForSeconds(m_timeOn);

        StartCoroutine(Off());
    }

    private IEnumerator Off()
    {
        for (int i = 0; i < m_lasers.Length; i++)
        {
            m_lasers[i].SetActive(false);
        }

        yield return new WaitForSeconds(m_timeOff);

        StartCoroutine(On());
    }
}
