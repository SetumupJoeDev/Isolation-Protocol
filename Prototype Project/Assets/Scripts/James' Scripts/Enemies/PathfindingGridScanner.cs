using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PathfindingGridScanner : MonoBehaviour
{

    public AstarPath m_pathFinder;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.hello.levelLoadingComplete += ScanNewGrid;
    }

    public void ScanNewGrid( )
    {
        m_pathFinder.Scan( );
    }

}
