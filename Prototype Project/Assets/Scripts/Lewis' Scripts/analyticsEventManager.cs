using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analyticsEventManager : MonoBehaviour
{
    public static analyticsEventManager analytics;

    private void Awake()
    {
        analytics = this;
    }



    public event Action<String> bulletShootIncrement;

    public event Action<String> bulletHitIncrement;


    public event Action<String> enemyDeathIncrement;

    public event Action<String> countEnemyIncrement;

    public event Action<String> onEnemyAttackIncrement;


    public event Action<String> onBuyItemIncrement;

    public event Action<analyticsManager> onAnalyticsPass;

    public event Action onPlayerDeath;

    public event Action<CurrencyManager> CurrencyIncrement;

    public event Action onCurrencyIncrement;

    public void whenCurrencyIncrement()
    {
        onCurrencyIncrement?.Invoke();
    }

    public void onCurrencyCall(CurrencyManager script)
    {
        CurrencyIncrement?.Invoke(script);
    }
    public void playerDeathMethod()
    {
        onPlayerDeath?.Invoke();
    }

    public void passAnalytics(analyticsManager script)
    {
        onAnalyticsPass?.Invoke(script);
    }


   

public void onBulletShoot(string str)
    {
        bulletShootIncrement?.Invoke(str);
    }

public void onBulletHit(string string1)
    {
        bulletHitIncrement?.Invoke(string1);
    }


    public void onEnemyDeath(string string2)
    {
        enemyDeathIncrement?.Invoke(string2);
    }
    public void onEnemySpawn(string string3)
    {
        countEnemyIncrement?.Invoke(string3);
    }

    public void onEnemyAttack(string name)
    {
        onEnemyAttackIncrement?.Invoke(name);
    }


public void onBuyItem(string name)
    {
        onBuyItemIncrement?.Invoke(name);
    }   
}
