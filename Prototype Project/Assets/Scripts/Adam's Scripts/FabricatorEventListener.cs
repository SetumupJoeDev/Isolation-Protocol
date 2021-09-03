using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorEventListener : MonoBehaviour
{
    public static FabricatorEventListener current;

    private void Awake()
    {
        current = this;
    }

    public event Action onFabricatorProductUnlock;
    public void FabricatorProductUnlock()
    {
        if(onFabricatorProductUnlock != null)
        {
            onFabricatorProductUnlock( );
        }
    }

    public event Action<string> onPlayerUpgradeUnlock;
    public void PlayerUpgradeUnlock(string itemName)
    {
        if(onPlayerUpgradeUnlock != null)
        {
            onPlayerUpgradeUnlock(itemName);
        }
    }
}
