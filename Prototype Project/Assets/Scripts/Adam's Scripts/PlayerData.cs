using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int      m_playerFuel;
    public string[] m_equippedWeapons;

    public PlayerData(PlayerController player)
    {
        m_playerFuel = player.m_currencyManager.m_fabricatorFuelCount;

        m_equippedWeapons = new string[player.m_carriedWeapons.Length];

        for (int i = 0; i < player.m_carriedWeapons.Length; i++)
        {
            m_equippedWeapons[i] = player.m_carriedWeapons[i].name;
        }
    }
}
