using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Sprite               m_emptyHeart;
    public Sprite               m_halfHeart;
    public Sprite               m_fullHeart;
    public Image[]              m_hearts;
    public Text                 m_dashCooldown;
    public Text                 m_weaponName;
    public Text                 m_numberOfCigs;
    public Text                 m_amountOfFuel;
    [HideInInspector]
    public bool                 m_dashOnCooldown;

    private PlayerController  m_playerController;
    private HealthManager     m_playerHealth;
    private CurrencyManager   m_playerCurrency;
    private int               m_playerMaxHealth;
    private int               m_playerCurrentHealth;
    private float             m_cooldownCounter;

    private void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_playerHealth = GetComponentInParent<HealthManager>();
        m_playerCurrency = GetComponentInParent<CurrencyManager>();
        m_playerMaxHealth = m_playerHealth.m_maxHealth;
        m_cooldownCounter = m_playerController.m_dashCooldown;
    }

    private void Update()
    {
        m_playerCurrentHealth = m_playerHealth.m_currentHealth;
        m_dashCooldown.text = m_cooldownCounter.ToString("F2");

        m_weaponName.text = m_playerController.m_currentWeapon.name;

        m_numberOfCigs.text = m_playerCurrency.m_cigarettePacksCount.ToString();
        m_amountOfFuel.text = m_playerCurrency.m_fabricatorFuelCount.ToString();

        UpdateHearts(); 
        
        if (m_dashOnCooldown)
        {
            m_cooldownCounter -= Time.deltaTime;
        }
        else
        {
            m_dashCooldown.gameObject.SetActive(false);
        }
    }

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

    public void DashCooldown()
    {
        m_cooldownCounter = m_playerController.m_dashCooldown;
        m_dashOnCooldown = true;
        m_dashCooldown.gameObject.SetActive(true);
    }
}
