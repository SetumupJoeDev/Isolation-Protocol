using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEvents : MonoBehaviour
{

    public static gameEvents hello;

    private void Awake()
    {
        hello = this;
    }

    public event Action enemyTargetPlayer;

    public void runEnemyTargetPlayer()
    {
        enemyTargetPlayer?.Invoke();
    }

    public event Action goodBye;

    public void runGoodbye()
    {
        goodBye?.Invoke();
    }
}
