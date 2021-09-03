using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int      m_maxHealth;
    public int      m_playerFuel;
    public string[] m_equippedWeapons;
    public bool     m_isDroneActive;
    public bool[]   m_droneModes;

    public PlayerData(PlayerController player)
    {
        m_maxHealth = player.m_playerHealthManager.m_maxHealth;

        m_playerFuel = player.m_currencyManager.m_fabricatorFuelCount;

        m_equippedWeapons = new string[player.m_carriedWeapons.Length];

        for (int i = 0; i < player.m_carriedWeapons.Length; i++)
        {
            m_equippedWeapons[i] = player.m_carriedWeapons[i].name;
        }

        m_isDroneActive = player.m_drone.gameObject.activeSelf;

        m_droneModes = new bool[player.m_drone.m_droneUpgrades.Length];

        for (int i = 0; i < player.m_drone.m_droneUpgrades.Length; i++)
        {
            m_droneModes[i] = player.m_drone.m_droneUpgrades[i].enabled;
        }
    }
}
