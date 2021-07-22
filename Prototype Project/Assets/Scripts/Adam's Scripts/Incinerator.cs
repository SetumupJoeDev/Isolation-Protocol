using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MonoBehaviour
{
    public bool           m_on;
    public ParticleSystem m_vfx;
    public GameObject     m_openGate;
    public GameObject     m_closedGate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(On());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator On()
    {
        m_on = true;
        m_vfx.Play();
        m_openGate.SetActive(true);
        m_closedGate.SetActive(false);
        yield return new WaitForSeconds(3);
        StartCoroutine(Off());
    }

    private IEnumerator Off()
    {
        m_on = false;
        m_vfx.Stop();
        m_closedGate.SetActive(true);
        m_openGate.SetActive(false);
        yield return new WaitForSeconds(3);
        StartCoroutine(On());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            CharacterBase collisionChar = collision.GetComponent<CharacterBase>();
            
            if (m_on && !collisionChar.m_isBurning)
            {
                collisionChar.m_isOnFire = true;
                collisionChar.m_onFireParticles.Play();
            }
        }
    }
}
