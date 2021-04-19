using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Enums.Directions m_doorDirection;
    
    protected RoomTemplates m_templates;
    protected int           m_random;
    protected bool          m_spawned;
    protected GameObject    m_spawnedRoom;

    protected virtual void Start()
    {
        m_spawned = false;
        m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    protected virtual void Spawn()
    {
        if (m_spawned == false)
        {
            switch (m_doorDirection)
            {
                case Enums.Directions.Left:
                    m_random = Random.Range(0, m_templates.m_rightRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_rightRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Right;
                    break;
                case Enums.Directions.Right:
                    m_random = Random.Range(0, m_templates.m_leftRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_leftRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Left;
                    break;
                case Enums.Directions.Top:
                    m_random = Random.Range(0, m_templates.m_bottomRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_bottomRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Bottom;
                    break;
                case Enums.Directions.Bottom:
                    m_random = Random.Range(0, m_templates.m_topRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_topRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Top;
                    break;
            }

            m_spawned = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>().m_spawned == false && !m_spawned)
            {
                Destroy(gameObject);
            }

            m_spawned = true;
        }
    }
}
