using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeEvents : MonoBehaviour
{
    public static PlayerUpgradeEvents m_playerUpgradeEvents;

    private void Awake()
    {
        m_playerUpgradeEvents = this;
    }

    public event Action onOnTurboLegsUnlock;
    public void ShieldUnlock()
    {
        if(onOnTurboLegsUnlock != null)
        {
            onOnTurboLegsUnlock();
        }
    }
}
