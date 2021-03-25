using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [SerializeField]
    [Tooltip("The health this unit currently has")]
    protected float currentHealth;
    [Tooltip("The maximum amount of health this unit can have")]
    protected float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }

    public void Heal(float healAmount)
    {
        currentHealth = currentHealth + healAmount;
    }
}
