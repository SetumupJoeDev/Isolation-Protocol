﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [SerializeField]
    [Tooltip("The health this unit currently has")]
    public float currentHealth;
    [SerializeField]
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
        currentHealth -= damage;
        if( currentHealth <= 0 )
        {
            Destroy( gameObject );
        }
        Debug.Log( "TakeDamage called!" );
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
    }
}
