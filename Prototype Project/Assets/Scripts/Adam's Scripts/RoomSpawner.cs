using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [HideInInspector]
    public Enums.Directions     m_doorDirection;
    public LayerMask            m_wallLayer;

    protected RoomTemplates     m_templates;
    protected LevelController   m_levelController;
    protected int               m_random;
    protected bool              m_spawned;
    protected GameObject        m_spawnedRoom;
    protected bool              m_wallAbove;
    protected bool              m_wallBelow;
    protected bool              m_wallRight;
    protected bool              m_wallLeft;


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

    protected virtual void Spawn()
    {
        if (!m_spawned && !m_levelController.m_reachedLimit)
        {
            switch (m_doorDirection)
            {
                case Enums.Directions.Left:
                    m_random = Random.Range(0, m_templates.m_rightDoorRooms.Length);
                    //m_templates.m_rightDoorRooms[m_random].GetComponent
                    m_spawnedRoom = Instantiate(m_templates.m_rightDoorRooms[m_random], transform.position, Quaternion.identity);
                    break;
                case Enums.Directions.Right:
                    m_random = Random.Range(0, m_templates.m_leftDoorRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_leftDoorRooms[m_random], transform.position, Quaternion.identity);
                    break;
                case Enums.Directions.Top:
                    m_random = Random.Range(0, m_templates.m_bottomDoorRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_bottomDoorRooms[m_random], transform.position, Quaternion.identity);
                    break;
                case Enums.Directions.Bottom:
                    m_random = Random.Range(0, m_templates.m_topDoorRooms.Length);
                    m_spawnedRoom = Instantiate(m_templates.m_topDoorRooms[m_random], transform.position, Quaternion.identity);
                    break;
            }

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
                Destroy(gameObject);
            }

            m_spawned = true;
        }
    }

    protected void CheckSurroudings()
    {
        if(Physics2D.Raycast(transform.position, Vector2.up, 10f, m_wallLayer))
        {
            m_wallAbove = true;
        }
        if(Physics2D.Raycast(transform.position, Vector2.down, 10f, m_wallLayer))
        {
            m_wallBelow = true;
        }
        if(Physics2D.Raycast(transform.position, Vector2.left, 10f, m_wallLayer))
        {
            m_wallLeft = true;
        }
        if(Physics2D.Raycast(transform.position, Vector2.right, 10f, m_wallLayer))
        {
            m_wallRight = true;
        }
    }
}
