using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int          m_playerFuel;
    public GameObject[] m_equippedWeapons;

    public PlayerData(PlayerController player)
    {
        m_playerFuel = player.m_currencyManager.m_fabricatorFuelCount;

        m_equippedWeapons = player.m_carriedWeapons;
    }
}
