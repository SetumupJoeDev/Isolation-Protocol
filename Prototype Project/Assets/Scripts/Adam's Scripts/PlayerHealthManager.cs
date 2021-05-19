using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField]
    private GameObject playerHUD;
    [SerializeField]
    private GameObject deathUI;
    [SerializeField]
    private GameObject deathCamera;

    public GameObject m_bloodEffect;            // Lewis' code.
    public AudioClip m_playerDamagedFeedback;   // Lewis' code.

    public override void TakeDamage(int damage)
    {
        if (m_isVulnerable)
        {
            StartCoroutine(PlayerDamageFeedBack()); // Lewis' code. See co-routine below
            m_currentHealth -= damage;
            m_isVulnerable = false;
            StartCoroutine(DamagedInvunerability());
        }
        if(m_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerHUD.SetActive(false);
        Instantiate(deathCamera);
        Instantiate(deathUI);
        deathCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10); 
        gameObject.SetActive(false);
    }

    IEnumerator DamagedInvunerability()
    {
        yield return new WaitForSeconds(0.5f);
        m_isVulnerable = true;
    }

    IEnumerator PlayerDamageFeedBack()
    {
        if (gameObject.name == "Player")
        {
            AudioSource.PlayClipAtPoint(m_playerDamagedFeedback, transform.position);
            m_bloodEffect.SetActive(true);
            yield return new WaitForSecondsRealtime(0.4f);
            m_bloodEffect.SetActive(false);
        }
    } // Lewis' code. Activates a blood animation on the player when damaged
}
