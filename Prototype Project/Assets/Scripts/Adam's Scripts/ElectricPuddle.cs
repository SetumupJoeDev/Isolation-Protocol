using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPuddle : MonoBehaviour
{
    public ParticleSystem   m_particles;
    public bool             m_electrified;

    private bool            m_caught;
    private CharacterBase   m_caughtThing;

    private void Start()
    {
        m_electrified = false;
        StartCoroutine(Electrified());
    }

    private void Update()
    {
        if(m_electrified && m_caught)
        {
            StartCoroutine(m_caughtThing.Stun());
        }
    }

    private IEnumerator Electrified()
    {
        m_electrified = true;
        m_particles.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(NotElectrified());
    }

    private IEnumerator NotElectrified()
    {
        m_electrified = false;
        yield return new WaitForSeconds(3);
        StartCoroutine(Electrified());
    }

    // TODO: Make into OnTriggerStay and m_caughtThing into an array 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            m_caught = true;
            m_caughtThing = collision.GetComponent<CharacterBase>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            m_caught = false;
        }
    }
}
