using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PathfindingGridScanner : MonoBehaviour
{

    [Tooltip("The A* pathfinding grid component attached to this object.")]
    public AstarPath m_pathFinder;

    // Start is called before the first frame update
    void Start()
    {
        //Subscribes the grid scanning method to the level load complete event so that the level scans a new grid only after all rooms have been spawned
        gameEvents.hello.levelLoadingComplete += ScanNewGrid;
    }

    public void ScanNewGrid( )
    {
        //Scans the scene to create an up-to-date pathfinding grid
        m_pathFinder.Scan( );
    }

}
