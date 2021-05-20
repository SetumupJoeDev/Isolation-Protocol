using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the player Heads-Up Display functionality
public class HUDManager : MonoBehaviour
{
    [Header("Sprites")]

    [Tooltip("The empty heart sprite")]
    [SerializeField]
    private Sprite              m_emptyHeart;

    [Tooltip("The half heart sprite")]
    [SerializeField]
    private Sprite              m_halfHeart;

    [Tooltip("The full heart sprite")]
    [SerializeField]
    private Sprite              m_fullHeart;
    
    [Header("HUD Elements")]

    [Tooltip("The HUD health display heart images")]
    [SerializeField]
    private Image[]             m_hearts;

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
        
        // Updates text on text objects
        m_dashCooldown.text = m_cooldownTimer.ToString("F2");

        m_weaponName.text = m_playerController.m_currentWeapon.name;

        m_numberOfCigs.text = m_playerCurrency.m_cigarettePacksCount.ToString();
        m_amountOfFuel.text = m_playerCurrency.m_fabricatorFuelCount.ToString();

        UpdateHearts(); 
        
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

    // Manually changes the displayed heart sprites based on player current health
    // (currently made specifically for when player has 6 max health as there are no current plans to change the max health of the player in anyway)
    private void UpdateHearts()
    {
        if (m_playerCurrentHealth == m_playerMaxHealth)
        {
            for (int i = 0; i < m_hearts.Length; i++)
            {
                m_hearts[i].sprite = m_fullHeart;
            }
        }
        if (m_playerCurrentHealth == 5)
        {
            m_hearts[0].sprite = m_fullHeart;
            m_hearts[1].sprite = m_fullHeart;
            m_hearts[2].sprite = m_halfHeart;
        }
        if (m_playerCurrentHealth == 4)
        {
            m_hearts[0].sprite = m_fullHeart;
            m_hearts[1].sprite = m_fullHeart;
            m_hearts[2].sprite = m_emptyHeart;
        }
        if (m_playerCurrentHealth == 3)
        {
            m_hearts[0].sprite = m_fullHeart;
            m_hearts[1].sprite = m_halfHeart;
            m_hearts[2].sprite = m_emptyHeart;
        }
        if (m_playerCurrentHealth == 2)
        {
            m_hearts[0].sprite = m_fullHeart;
            m_hearts[1].sprite = m_emptyHeart;
            m_hearts[2].sprite = m_emptyHeart;
        }
        if (m_playerCurrentHealth == 1)
        {
            m_hearts[0].sprite = m_halfHeart;
            m_hearts[1].sprite = m_emptyHeart;
            m_hearts[2].sprite = m_emptyHeart;
        }
        if (m_playerCurrentHealth == 0)
        {
            for (int i = 0; i < m_hearts.Length; i++)
            {
                m_hearts[i].sprite = m_emptyHeart;
            }
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
