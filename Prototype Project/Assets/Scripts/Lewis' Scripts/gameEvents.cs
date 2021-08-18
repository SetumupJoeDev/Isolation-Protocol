using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEvents : MonoBehaviour
{

    public static gameEvents hello;

    public event Action levelLoadingComplete;

    public event Action<LevelLoader, string> loadingNewLevel;

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

    public void levelLoaded( )
    {
        if( levelLoadingComplete != null )
        {
            levelLoadingComplete( );
        }
    }

    public void loadingLevel( LevelLoader levelController, string newLevelName )
    {
        if( loadingNewLevel != null )
        {
            loadingNewLevel( levelController, newLevelName );
        }
    }

}
