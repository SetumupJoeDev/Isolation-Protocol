using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [SerializeField]
    [Tooltip("The health this unit currently has")]
    public float m_currentHealth;
    [SerializeField]
    [Tooltip("The maximum amount of health this unit can have")]
    protected float m_maxHealth;

    [SerializeField]
    [Tooltip("Determines whether or not the player can currently take damage")]
    public bool m_isInvulnerable;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        if ( !m_isInvulnerable )
        {
            StartCoroutine(PlayerDamageFeedBack());
            m_currentHealth -= damage;
            
        }
    }

    public virtual void Heal(float healAmount)
    {
        m_currentHealth += healAmount;
    }


    IEnumerator PlayerDamageFeedBack()
    {
        GameObject blood = gameObject.transform.GetChild(3).gameObject;
        blood.SetActive(true);
        yield return new WaitForSecondsRealtime(0.4f);
        blood.SetActive(false);
    }
}
