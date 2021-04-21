﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositionAjustment : MonoBehaviour
{
    [HideInInspector]
    public Enums.Directions m_directionSpawnedFrom;
    public GameObject[]     m_spawnPoints;
    public float            m_roomWidth;
    public float            m_roomHeight;
    
    protected Vector3       m_ajustedPosition;
    protected Vector3       m_topDoorDistance;
    protected Vector3       m_leftDoorDistance;
    protected Vector3       m_rightDoorDistance;
    protected Vector3       m_bottomDoorDistance;

    protected void Start()
    {
        for (int i = 0; i < m_spawnPoints.Length; i++)
        {
            switch (m_spawnPoints[i].GetComponent<RoomSpawner>().m_doorDirection)
            {
                case Enums.Directions.Top:
                    m_topDoorDistance = m_spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Left:
                    m_leftDoorDistance = m_spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Right:
                    m_rightDoorDistance = m_spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Bottom:
                    m_bottomDoorDistance = m_spawnPoints[i].transform.localPosition;
                    break;
                default:
                    break;
            }
        }

        m_roomWidth = m_leftDoorDistance.magnitude + m_rightDoorDistance.magnitude;
        m_roomHeight = m_topDoorDistance.magnitude + m_bottomDoorDistance.magnitude;

        switch (m_directionSpawnedFrom)
        {
            case Enums.Directions.Top:
                m_ajustedPosition = transform.position - m_topDoorDistance;
                transform.position = m_ajustedPosition;
                break;
            case Enums.Directions.Left:
                m_ajustedPosition = transform.position - m_leftDoorDistance;
                transform.position = m_ajustedPosition;
                break;
            case Enums.Directions.Right:
                m_ajustedPosition = transform.position - m_rightDoorDistance;
                transform.position = m_ajustedPosition;
                break;
            case Enums.Directions.Bottom:
                m_ajustedPosition = transform.position - m_bottomDoorDistance;
                transform.position = m_ajustedPosition;
                break;
            default:
                Debug.Log("Went to default");
                break;
        }
    }
}