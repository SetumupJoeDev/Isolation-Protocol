using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FabricatorData
{
    public bool[] m_droneUpgradesUnlocks;
    public bool[] m_exoSuitUpgradesUnlocks;
    public bool[] m_weaponUnlocks;

    public FabricatorData(FabricatorUpgradeListGenerator droneList, FabricatorUpgradeListGenerator exoSuitList, FabricatorUpgradeListGenerator weaponList)
    {
        m_droneUpgradesUnlocks = new bool[droneList.m_buttonObjectReferences.Length];
        m_exoSuitUpgradesUnlocks = new bool[exoSuitList.m_buttonObjectReferences.Length];
        m_weaponUnlocks = new bool[weaponList.m_buttonObjectReferences.Length];

        for (int i = 0; i < droneList.m_buttonObjectReferences.Length; i++)
        {
            m_droneUpgradesUnlocks[i] = droneList.m_buttonObjectReferences[i].GetIsUnlocked();
        }

        for (int i = 0; i < exoSuitList.m_buttonObjectReferences.Length; i++)
        {
            m_exoSuitUpgradesUnlocks[i] = exoSuitList.m_buttonObjectReferences[i].GetIsUnlocked();
        }

        for (int i = 0; i < weaponList.m_buttonObjectReferences.Length; i++)
        {
            m_weaponUnlocks[i] = weaponList.m_buttonObjectReferences[i].GetIsUnlocked();
        }
    }
}
