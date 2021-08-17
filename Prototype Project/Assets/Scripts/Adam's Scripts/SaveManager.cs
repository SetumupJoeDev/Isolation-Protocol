using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerController               m_player;
    public FabricatorUpgradeListGenerator m_droneUpgrades;
    public FabricatorUpgradeListGenerator m_exoSuitUpgrades;
    public FabricatorUpgradeListGenerator m_weaponUnlocks;

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        SaveSystem.SavePlayer(m_player);
        SaveSystem.SaveFabricator(m_droneUpgrades, m_exoSuitUpgrades, m_weaponUnlocks);
        Debug.Log(Application.persistentDataPath);
    }

    public void Load()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        m_player.m_currencyManager.m_fabricatorFuelCount = playerData.m_playerFuel;


        FabricatorData FabricatorData = SaveSystem.LoadFabricator();
        
        for (int i = 0; i < m_droneUpgrades.m_buttonObjectReferences.Length; i++)
        {
            m_droneUpgrades.m_buttonObjectReferences[i].SetIsUnlocked(FabricatorData.m_droneUpgradesUnlocks[i]);
        }
        
        for (int i = 0; i < m_exoSuitUpgrades.m_buttonObjectReferences.Length; i++)
        {
            m_exoSuitUpgrades.m_buttonObjectReferences[i].SetIsUnlocked(FabricatorData.m_exoSuitUpgradesUnlocks[i]);
        }
        
        for (int i = 0; i < m_weaponUnlocks.m_buttonObjectReferences.Length; i++)
        {
            m_weaponUnlocks.m_buttonObjectReferences[i].SetIsUnlocked(FabricatorData.m_weaponUnlocks[i]);
        }
    }
}