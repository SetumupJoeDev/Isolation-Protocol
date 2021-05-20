using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class to manage the health of player and enemies
public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [Tooltip("The health this unit currently has")]
    public int  m_currentHealth;

    [Tooltip("The maximum amount of health this unit can have")]
    public int  m_maxHealth;

    [Header("Vulnerability")]
    
    [Tooltip("Determines whether or not the unit can currently take damage")]
    public bool m_isVulnerable;

    // Unit starts vulnerable and with max health
    public virtual void Start()
    {
        m_currentHealth = m_maxHealth;
        m_isVulnerable = true;
    }

    // Lowers the current health of the unit by given damage amount if vulnerable
    public virtual void TakeDamage(int damage)
    {
        if ( m_isVulnerable )
        {
            m_currentHealth -= damage;
        }
    }

    // Raises the current health of the unit by given heal amount if unit not at max health
    public virtual void Heal(int healAmount)
    {
        if(m_currentHealth < m_maxHealth)
        {
            m_currentHealth += healAmount;
        }
    }
}
