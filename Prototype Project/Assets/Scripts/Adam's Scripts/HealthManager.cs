using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [Tooltip("The health this unit currently has")]
    public int  m_currentHealth;
    [Tooltip("The maximum amount of health this unit can have")]
    public int  m_maxHealth;

    [Tooltip("Determines whether or not the player can currently take damage")]
    public bool m_isInvulnerable;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if ( !m_isInvulnerable )
        {
            StartCoroutine(PlayerDamageFeedBack()); // Lewis' code. See co-routine below
            m_currentHealth -= damage;
            
        }
    }

    public virtual void Heal(int healAmount)
    {
        m_currentHealth += healAmount;
    }


    IEnumerator PlayerDamageFeedBack()
    {
        GameObject blood = gameObject.transform.GetChild(3).gameObject;
        blood.SetActive(true);
        yield return new WaitForSecondsRealtime(0.4f);
        blood.SetActive(false);
    } // Lewis' code. Activates a blood animation on the player when damaged
}
