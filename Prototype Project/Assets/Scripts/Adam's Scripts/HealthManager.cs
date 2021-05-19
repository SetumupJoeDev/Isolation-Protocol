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

    [Tooltip("Determines whether or not the unit can currently take damage")]
    public bool m_isVulnerable;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_currentHealth = m_maxHealth;
        m_isVulnerable = true;
    }

    public virtual void TakeDamage(int damage)
    {
        if ( m_isVulnerable )
        {
            m_currentHealth -= damage;
        }
    }

    public virtual void Heal(int healAmount)
    {
        if(m_currentHealth < m_maxHealth)
        {
            m_currentHealth += healAmount;
        }
    }
}
