using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

// Manages the player character's health and specific methods needed
public class PlayerHealthManager : HealthManager
{
    [Tooltip("The player HUD canvas")]
    [SerializeField]
    private GameObject  m_playerHUD;
    
    [Tooltip("The death UI canvas")]
    [SerializeField]
    private GameObject  m_deathUI;
    
    [Tooltip("The death camera")]
    [SerializeField]
    private GameObject  m_deathCamera; // Used to continue showing the game once the player object is either destroyed or deactivated upon death

    public GameObject   m_bloodEffect;            // Lewis' code.
    public AudioClip    m_playerDamagedFeedback;   // Lewis' code.

    public enum playerState { alive, dead, downed}

    public playerState m_currentPlayerState;

    public analyticsManager analytics;


    // Lowers player health by amount given if vulnerable
    public override void TakeDamage(int damage)
    {
        if (m_isVulnerable)
        {
            StartCoroutine(PlayerDamageFeedBack()); // Lewis' code. See co-routine below
            m_currentHealth -= damage;
            
            // Makes player invulnerable for short time after getting damaged
            m_isVulnerable = false;
            StartCoroutine(DamagedInvunerability()); 
        }

        // Call player death method when out of health
        if( m_currentHealth <= 0 )
        {
            if (GameObject.Find("easyName").GetComponent<analyticsManager>() != null)
            {

                analytics = GameObject.Find("easyName").GetComponent<analyticsManager>();

                analytics.onDeath();
            }


            if (  !GetComponentInChildren<DroneController>().gameObject.activeSelf || !gameObject.GetComponentInChildren<DefibMode>( ).enabled || !gameObject.GetComponentInChildren<DefibMode>( ).m_canDefibPlayer )
            {
                Die( );
            }
            else if ( gameObject.GetComponentInChildren<DefibMode>( ).m_canDefibPlayer )
            {
                m_currentPlayerState = playerState.downed;
                gameObject.GetComponentInChildren<DefibMode>( ).AttemptDefibrillation( );
            }
        }
        
    }

    // Deactivates player and sets up death screen
    private void Die()
    {
       
       


        Instantiate(m_deathCamera);
        Instantiate(m_deathUI);
        
        // Sets camera position to players position when they died
        m_deathCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10); 
        gameObject.SetActive(false);
    }

    // Makes player invulnerable for a short time
    IEnumerator DamagedInvunerability()
    {
        yield return new WaitForSeconds(0.75f);
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
