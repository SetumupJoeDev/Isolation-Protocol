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

    protected PlayerController  m_playerController;
    protected HealthManager     m_playerHealth;
    protected int               m_playerMaxHealth;
    protected int               m_playerCurrentHealth;

    protected void Start()
    {
        m_playerHealth = GetComponentInParent<HealthManager>();
        m_playerMaxHealth = m_playerHealth.m_maxHealth;
    }

    protected void Update()
    {
        m_playerCurrentHealth = m_playerHealth.m_currentHealth;
        UpdateHearts();
    }

    protected void UpdateHearts()
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
}
