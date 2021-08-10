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

                m_levelController.m_numberOfCombatRooms++;
            }
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
            
            // Assigns this as the spawner the spawned room was spawned from
            m_spawnedRoom.GetComponent<RoomController>().m_spawnedFrom = this;

            // This spawner now has spawned a room
            m_spawned = true;

            // Add the spawned room to the room list-
            m_levelController.m_roomList.Add(m_spawnedRoom);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            // Upon collision with another room spawn point that has not spawned a room either 
            // will determine correct room to spawn based on both spawn point door directions so that all 3 rooms connect
            if (collision.GetComponent<RoomSpawner>().m_spawned == false && !m_spawned)
            {
                switch (m_doorDirection)
                {
                    case Enums.Directions.Top:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Left:
                                m_random = Random.Range(0, m_templates.m_bottomRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_bottomRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_random = Random.Range(0, m_templates.m_bottomLeftDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_bottomLeftDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_random = Random.Range(0, m_templates.m_topBottomDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topBottomDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Left:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_random = Random.Range(0, m_templates.m_bottomRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_bottomRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_random = Random.Range(0, m_templates.m_leftRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_leftRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_random = Random.Range(0, m_templates.m_topRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Right:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_random = Random.Range(0, m_templates.m_bottomLeftDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_bottomLeftDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_random = Random.Range(0, m_templates.m_leftRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_leftRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Bottom:
                                m_random = Random.Range(0, m_templates.m_topLeftDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topLeftDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case Enums.Directions.Bottom:
                        switch (collision.GetComponent<RoomSpawner>().m_doorDirection)
                        {
                            case Enums.Directions.Top:
                                m_random = Random.Range(0, m_templates.m_topBottomDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topBottomDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Left:
                                m_random = Random.Range(0, m_templates.m_topRightDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topRightDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                            case Enums.Directions.Right:
                                m_random = Random.Range(0, m_templates.m_topLeftDoorRooms.Length);
                                m_spawnedRoom = Instantiate(m_templates.m_topLeftDoorRooms[m_random], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                }

                m_spawnedRoom.GetComponent<RoomController>().m_spawnedFrom = this;
                m_spawned = true;
                m_levelController.m_numberOfCombatRooms++;
                m_levelController.m_roomList.Add(m_spawnedRoom);
                collision.gameObject.SetActive(false);      // Deactivates collided spawn point to stop collison from happening again
            }
            else if (!m_spawned)
            {
                // Prevents this spawn point from spawning a room on top of another previously spawned room
                m_spawned = true;
                gameObject.SetActive(false);
            }
        }
    }
}
