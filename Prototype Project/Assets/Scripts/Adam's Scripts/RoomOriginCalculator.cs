using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOriginCalculator : MonoBehaviour
{
    public GameObject spawnPointLeft;
    public GameObject spawnPointRight;
    public GameObject spawnPointTop;
    public GameObject spawnPointBottom;

    [HideInInspector]
    public Vector3 spawnPointLeftPosition;
    [HideInInspector]
    public Vector3 spawnPointRightPosition;

    protected void Start()
    {
        spawnPointLeftPosition = spawnPointLeft.transform.localPosition;
        spawnPointRightPosition = spawnPointRight.transform.localPosition;
    }
}
