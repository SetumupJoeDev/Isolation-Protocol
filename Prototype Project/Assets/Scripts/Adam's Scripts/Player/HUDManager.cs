using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the player Heads-Up Display functionality
public class HUDManager : MonoBehaviour
{
    [Header("Sprites")]

    [Tooltip("The health bar sprites")]
    [SerializeField]
    private Sprite[]            m_healthSprites;
    
    [Header("HUD Elements")]

    [Tooltip("The HUD health display image")]
    [SerializeField]
    private Image               m_healthBar;

    [Tooltip("The dash cooldown text object")]
    [SerializeField]
    private Text                m_dashCooldown;
    
    [Tooltip("The weapon name text object")]
    [SerializeField]
    private Text                m_weaponName;

    [Tooltip("The 'Cigs' text object")]
    [SerializeField]
    private Text                m_numberOfCigs;

    [Tooltip("The 'Fuel' text object")]
    [SerializeField]
    private Text                m_amountOfFuel;

    // Whether or not the dash is on cooldown
    [HideInInspector]
    public bool                 m_dashOnCooldown;

    // Reference to player controller
    private PlayerController    m_playerController;

    // Reference to player health manager
    private HealthManager       m_playerHealth;

    // Reference to player currency manager
    private CurrencyManager     m_playerCurrency;

    // Player max health
    private int                 m_playerMaxHealth;
    
    // Player current health
    private int                 m_playerCurrentHealth;
    
    // Player dash cooldown countdown timer
    private float               m_cooldownTimer;

    // Basic reference setup
    private void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_playerHealth = GetComponentInParent<HealthManager>();
        m_playerCurrency = GetComponentInParent<CurrencyManager>();
        m_playerMaxHealth = m_playerHealth.m_maxHealth;
        m_cooldownTimer = m_playerController.m_dashCooldown;
    }

    private void Update()
    {
        m_playerCurrentHealth = m_playerHealth.m_currentHealth;

        m_healthBar.sprite = m_healthSprites[m_playerCurrentHealth];

        // Updates text on text objects
        m_dashCooldown.text = m_cooldownTimer.ToString("F2");

        if(m_weaponName != null)
        {
            m_weaponName.text = m_playerController.m_currentWeapon.name;
        }

        m_numberOfCigs.text = m_playerCurrency.m_cigarettePacksCount.ToString();
        m_amountOfFuel.text = m_playerCurrency.m_fabricatorFuelCount.ToString();

        // Ticks down timer if dash is on cooldown
        if (m_dashOnCooldown)
        {
            m_cooldownTimer -= Time.deltaTime;
        }
        else
        {
            m_dashCooldown.gameObject.SetActive(false);
        }
    }

    // Activates and sets off the the HUD dash cooldown timer
    public void DashCooldown()
    {
        m_cooldownTimer = m_playerController.m_dashCooldown;
        m_dashOnCooldown = true;
        m_dashCooldown.gameObject.SetActive(true);
    }
}
