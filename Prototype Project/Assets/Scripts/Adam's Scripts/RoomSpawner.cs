﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [HideInInspector]
    public Enums.Directions     m_doorDirection;
    [HideInInspector]
    public LevelController      m_levelController;
    public bool                 m_spawned;

    protected RoomTemplates     m_templates;
    protected int               m_random;
    protected GameObject        m_spawnedRoom;

    protected virtual void Start()
    {
        m_spawned = false;
        m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        m_levelController = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<LevelController>();
        if(transform.localPosition.x > 0 && transform.localPosition.y == 0)
        {
            m_doorDirection = Enums.Directions.Right;
        }
        if(transform.localPosition.x < 0 && transform.localPosition.y == 0)
        {
            m_doorDirection = Enums.Directions.Left;
        }
        if (transform.localPosition.y > 0 && transform.localPosition.x == 0)
        {
            m_doorDirection = Enums.Directions.Top;
        }
        if (transform.localPosition.y < 0 && transform.localPosition.x == 0)
        {
            m_doorDirection = Enums.Directions.Bottom;
        }
        Invoke("Spawn", 0.25f);
    }

    public virtual void Spawn()
    {
        if (!m_spawned)
        {
            if (!m_levelController.m_reachedLimit)
            {
                switch (m_doorDirection)
                {
                    case Enums.Directions.Top:
                        m_random = Random.Range(0, m_templates.m_bottomDoorRooms.Length);
                        m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRooms[m_random], transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Left:
                        m_random = Random.Range(0, m_templates.m_rightDoorRooms.Length);
                        m_spawnedRoom = Instantiate(m_templates.m_rightDoorRooms[m_random], transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Right:
                        m_random = Random.Range(0, m_templates.m_leftDoorRooms.Length);
                        m_spawnedRoom = Instantiate(m_templates.m_leftDoorRooms[m_random], transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Bottom:
                        m_random = Random.Range(0, m_templates.m_topDoorRooms.Length);
                        m_spawnedRoom = Instantiate(m_templates.m_topDoorRooms[m_random], transform.position, Quaternion.identity);
                        break;
                }
            }
            else
            {
                switch (m_doorDirection)
                {
                    case Enums.Directions.Top:
                        m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRoom, transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Left:
                        m_spawnedRoom = Instantiate(m_templates.m_rightDoorRoom, transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Right:
                        m_spawnedRoom = Instantiate(m_templates.m_leftDoorRoom, transform.position, Quaternion.identity);
                        break;
                    case Enums.Directions.Bottom:
                        m_spawnedRoom = Instantiate(m_templates.m_topDoorRoom, transform.position, Quaternion.identity);
                        break;
                }
            }

            m_spawnedRoom.GetComponent<RoomOpenings>().m_spawnedFrom = this;
            m_spawned = true;
            m_levelController.m_numberOfRooms++;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>().m_spawned == false && !m_spawned)
            {
                switch (m_doorDirection)
                {
                    case Enums.Directions.Top:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Left:
                                m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRooms[2], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRooms[1], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRooms[0], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Left:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_spawnedRoom = Instantiate(m_templates.m_rightDoorRooms[2], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_spawnedRoom = Instantiate(m_templates.m_rightDoorRooms[1], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_spawnedRoom = Instantiate(m_templates.m_rightDoorRooms[0], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Right:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_spawnedRoom = Instantiate(m_templates.m_leftDoorRooms[2], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_spawnedRoom = Instantiate(m_templates.m_leftDoorRooms[1], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_spawnedRoom = Instantiate(m_templates.m_leftDoorRooms[0], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Bottom:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_spawnedRoom = Instantiate(m_templates.m_topDoorRooms[2], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_spawnedRoom = Instantiate(m_templates.m_topDoorRooms[1], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_spawnedRoom = Instantiate(m_templates.m_topDoorRooms[0], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                }

                m_spawned = true;
                m_levelController.m_numberOfRooms++;
                collision.gameObject.SetActive(false);
            }

            m_spawned = true;
        }
    }
}
