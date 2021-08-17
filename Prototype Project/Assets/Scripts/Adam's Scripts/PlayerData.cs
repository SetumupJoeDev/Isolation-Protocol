using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int m_playerFuel;

    public PlayerData(PlayerController player)
    {
        m_playerFuel = player.m_currencyManager.m_fabricatorFuelCount;
    }
}
