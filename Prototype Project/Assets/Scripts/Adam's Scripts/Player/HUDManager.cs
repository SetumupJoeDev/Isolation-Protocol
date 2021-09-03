using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the player Heads-Up Display functionality
public class HUDManager : MonoBehaviour
{
    [Header("Sprites")]

    [Tooltip("The small health bar sprites")]
    [SerializeField]
    private Sprite[]            m_smallHealthSprites;

    [Tooltip("The medium health bar sprites")]
    [SerializeField]
    private Sprite[]            m_mediumHealthSprites;

    [Tooltip("The large heatlh bar sprites")]
    [SerializeField]
    private Sprite[]            m_fullHealthSprites;

    [Tooltip("The small blue health bar sprite")]
    [SerializeField]
    private Sprite              m_smallCombatArmour;

    [Tooltip("The medium blue health bar sprite")]
    [SerializeField]
    private Sprite              m_mediumCombatArmour;

    [Tooltip("The full blue health bar sprite")]
    [SerializeField]
    private Sprite              m_fullCombatArmour;
    
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
    private PlayerHealthManager m_playerHealth;

    // Reference to player currency manager
    private CurrencyManager     m_playerCurrency;

    // Player dash cooldown countdown timer
    private float               m_cooldownTimer;

    // Basic reference setup
    private void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_playerHealth = GetComponentInParent<PlayerHealthManager>();
        m_playerCurrency = GetComponentInParent<CurrencyManager>();
        m_cooldownTimer = m_playerController.m_dashCooldown;
    }

    private void Update()
    {
        switch (m_playerHealth.m_maxHealth)
        {
            case 6:
                if (m_playerHealth.m_hasCombatArmour)
                {
                    m_healthBar.sprite = m_smallCombatArmour;
                    m_healthBar.SetNativeSize();
                }
                else
                {
                    m_healthBar.sprite = m_smallHealthSprites[m_playerHealth.m_currentHealth];
                    m_healthBar.SetNativeSize();
                }
                break;
            case 8:
                if (m_playerHealth.m_hasCombatArmour)
                {
                    m_healthBar.sprite = m_mediumCombatArmour;
                    m_healthBar.SetNativeSize();
                }
                else
                {
                    m_healthBar.sprite = m_mediumHealthSprites[m_playerHealth.m_currentHealth];
                    m_healthBar.SetNativeSize();
                }
                break;
            case 10:
                if (m_playerHealth.m_hasCombatArmour)
                {
                    m_healthBar.sprite = m_fullCombatArmour;
                    m_healthBar.SetNativeSize();
                }
                else
                {
                    m_healthBar.sprite = m_fullHealthSprites[m_playerHealth.m_currentHealth];
                    m_healthBar.SetNativeSize();
                }
                break;
            default:
                break;
        }

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
