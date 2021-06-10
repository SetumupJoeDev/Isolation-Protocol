using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns a  randomly selected room
public class RoomSpawner : MonoBehaviour
{
    [Tooltip("The direction of the door that the spawned room should connect to")]
    [HideInInspector]
    public Enums.Directions     m_doorDirection;

    [Tooltip("The controller of the level this spawner is in")]
    [HideInInspector]
    public LevelController      m_levelController;

    [Tooltip("Whether or not this spawn point has spawned a room")]
    public bool                 m_spawned;

    // Room prefabs to be spawned
    private RoomTemplates     m_templates;
    private int               m_random;
    
    // Reference to the room this spawner has spawned
    private GameObject        m_spawnedRoom;

    private void Start()
    {
        m_spawned = false;
        m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        m_levelController = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<LevelController>();

        // Checks local position of spawn point relative to parent room to determine direction of door
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

        // Calls Spawn with a slight delay to avoid rooms spawning on top of each other
        Invoke("Spawn", 0.25f);
    }

    // Determines correct template to use and spawns a room from it
    public void Spawn()
    {
        if (!m_spawned)
        {
            // Spawns a random room from Basic rooms part of template while approximate room limit has not been reached
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
                // Once approximate room limit has been reached spawns an Exit room connected to one of the rooms
                if (!m_levelController.m_hasExitRoom)
                {
                    switch (m_doorDirection)
                    {
                        case Enums.Directions.Top:
                            m_spawnedRoom = Instantiate(m_templates.m_bottomExitRoom, transform.position, Quaternion.identity);
                            m_levelController.m_hasExitRoom = true;
                            break;
                        case Enums.Directions.Left:
                            m_spawnedRoom = Instantiate(m_templates.m_rightExitRoom, transform.position, Quaternion.identity);
                            m_levelController.m_hasExitRoom = true;
                            break;
                        case Enums.Directions.Right:
                            m_spawnedRoom = Instantiate(m_templates.m_leftExitRoom, transform.position, Quaternion.identity);
                            m_levelController.m_hasExitRoom = true;
                            break;
                        case Enums.Directions.Bottom:
                            // Due to its design the exit room has no top door variant
                            // So this spawns a dead end and leaves m_hasExitRoom as false so an exit room may be spawned at the next empty spawn point
                            m_spawnedRoom = Instantiate(m_templates.m_topDeadEndRoom, transform.position, Quaternion.identity);
                            m_levelController.m_hasExitRoom = false;
                            break;
                    }

                }
                else
                {
                    // Once an exit room has been spawned a shop room will be spawned
                    if (!m_levelController.m_hasShop)
                    {
                        switch (m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_spawnedRoom = Instantiate(m_templates.m_bottomShopRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_spawnedRoom = Instantiate(m_templates.m_rightShopRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_spawnedRoom = Instantiate(m_templates.m_leftShopRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_spawnedRoom = Instantiate(m_templates.m_topShopRoom, transform.position, Quaternion.identity);
                                break;
                        }

                        m_levelController.m_hasShop = true;
                    }
                    // Once both exit and shop rooms have been spawned any remaining empty spawn points spawn a dead end room to close the level
                    else
                    {
                        switch (m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_spawnedRoom = Instantiate(m_templates.m_bottomDeadEndRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_spawnedRoom = Instantiate(m_templates.m_rightDeadEndRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_spawnedRoom = Instantiate(m_templates.m_leftDeadEndRoom, transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_spawnedRoom = Instantiate(m_templates.m_topDeadEndRoom, transform.position, Quaternion.identity);
                                break;
                        }
                    }
                }
            }

            // Assigns this as the spawner the spawned room was spawned from
            m_spawnedRoom.GetComponent<RoomController>().m_spawnedFrom = this;
            
            // This spawner now has spawned a room
            m_spawned = true;

            // Add to the number of rooms spawned and add the spawned room to the room list-
            m_levelController.m_numberOfRooms++;
            m_levelController.m_roomList.Add(m_spawnedRoom);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            // Upon collision with another room spawn point that has not spawned a room either 
            // will determine correct room to spawn based on both spawn point door directions so that all 3 rooms connect
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

                m_spawnedRoom.GetComponent<RoomController>().m_spawnedFrom = this;
                m_spawned = true;
                m_levelController.m_numberOfRooms++;
                m_levelController.m_roomList.Add(m_spawnedRoom);
                collision.gameObject.SetActive(false);      // Deactivates collided spawn point to stop collison from happening again
            }

            // Prevents this spawn point from spawning a room on top of another previously spawned room
            m_spawned = true;
        }
    }
}
