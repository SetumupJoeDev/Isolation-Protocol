using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerController               m_player;
    public FabricatorUpgradeListGenerator m_droneUpgrades;
    public FabricatorUpgradeListGenerator m_exoSuitUpgrades;
    public FabricatorStoreProduct[]       m_weaponUnlocks;
    public WeaponWall                     m_weaponWall;

    private void Start()
    {
        FabricatorEventListener.current.onFabricatorProductUnlock += Save;
        Load();
    }

    public void Save()
    {
        SaveSystem.SavePlayer(m_player);
        SaveSystem.SaveFabricator(m_droneUpgrades, m_exoSuitUpgrades, m_weaponUnlocks);
        //Debug.Log(Application.persistentDataPath);
    }

    public void Load()
    {
        PlayerData playerData = SaveSystem.LoadPlayer(m_player);

        m_player.m_currencyManager.m_fabricatorFuelCount = playerData.m_playerFuel;

        for (int i = 0; i < playerData.m_equippedWeapons.Length; i++)
        {
            for (int f = 0; f < m_weaponWall.m_weaponPrefabs.Length; f++)
            {
                if( m_weaponWall.m_weaponPrefabs[f].name + "(Clone)" == playerData.m_equippedWeapons[i])
                {
                    // Weapong spawning code taken from WeaponWall.cs with minor adjustments
                    Destroy(m_player.m_carriedWeapons[i]);

                    //Instantiates a new weapon at the player's weapon hold point using the weapon prefab
                    m_player.m_carriedWeapons[i] = Instantiate(m_weaponWall.m_weaponPrefabs[f], m_player.m_weaponAttachPoint.transform.position, Quaternion.identity);

                    //Sets the new weapon's transform parent to the player's weapon hold point
                    m_player.m_carriedWeapons[i].transform.parent = m_player.m_weaponAttachPoint.transform;

                    //Sets the player's current weapon as the newly created weapon, making it look as though they had swapped weapons for it
                    m_player.m_currentWeapon = m_player.m_carriedWeapons[m_player.m_currentWeaponIndex];

                    //Disables the weapon so it can't be used in the hub area
                    m_player.m_carriedWeapons[i].SetActive(false);
                }
            }
        }

        FabricatorData fabricatorData = SaveSystem.LoadFabricator(m_droneUpgrades, m_exoSuitUpgrades, m_weaponUnlocks);

        for (int i = 0; i < m_droneUpgrades.m_buttonObjectReferences.Length; i++)
        {
            m_droneUpgrades.m_buttonObjectReferences[i].SetIsUnlocked(fabricatorData.m_droneUpgradesUnlocks[i]);
        }

        for (int i = 0; i < m_exoSuitUpgrades.m_buttonObjectReferences.Length; i++)
        {
            m_exoSuitUpgrades.m_buttonObjectReferences[i].SetIsUnlocked(fabricatorData.m_exoSuitUpgradesUnlocks[i]);
        }

        for (int i = 0; i < m_weaponUnlocks.Length; i++)
        {
            m_weaponUnlocks[i].SetIsUnlocked(fabricatorData.m_weaponUnlocks[i]);
        }
    }
}