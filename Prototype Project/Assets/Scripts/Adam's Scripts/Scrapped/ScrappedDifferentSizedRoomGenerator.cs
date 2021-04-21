using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class RoomSpawner : MonoBehaviour
{
    public Enums.Directions m_doorDirection;
    public LayerMask m_wallLayer;

    protected RoomTemplates m_templates;
    protected LevelController m_levelController;
    protected int m_random;
    protected bool m_spawned;
    protected GameObject m_spawnedRoom;

    protected virtual void Start()
    {
        m_spawned = false;
        m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        m_levelController = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<LevelController>();
        Invoke("Spawn", 0.25f);
    }

    protected virtual void Spawn()
    {
        if (!m_spawned && !m_levelController.m_reachedLimit)
        {
            switch (m_doorDirection)
            {
                case Enums.Directions.Left:
                    m_random = Random.Range(0, m_templates.m_rightRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_rightRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Right;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10f, m_wallLayer);
                    if (hit)
                    {
                        Destroy(m_spawnedRoom);
                    }
                    break;
                case Enums.Directions.Right:
                    m_random = Random.Range(0, m_templates.m_leftRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_leftRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Left;
                    hit = Physics2D.Raycast(transform.position, Vector2.right, 10f, m_wallLayer);
                    if (hit)
                    {
                        Destroy(m_spawnedRoom);
                    }
                    break;
                case Enums.Directions.Top:
                    m_random = Random.Range(0, m_templates.m_bottomRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_bottomRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Bottom;
                    hit = Physics2D.Raycast(transform.position, Vector2.up, 10f, m_wallLayer);
                    if (hit)
                    {
                        Destroy(m_spawnedRoom);
                    }
                    break;
                case Enums.Directions.Bottom:
                    m_random = Random.Range(0, m_templates.m_topRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_topRooms[m_random], transform.position, Quaternion.identity);
                    m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Top;
                    hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, m_wallLayer);
                    if (hit)
                    {
                        Destroy(m_spawnedRoom);
                    }
                    break;
            }

            m_spawned = true;
            m_levelController.m_numberOfRooms++;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().m_spawned == false && !m_spawned)
            {
                Destroy(gameObject);
            }

            m_spawned = true;
        }
    }
}
*/