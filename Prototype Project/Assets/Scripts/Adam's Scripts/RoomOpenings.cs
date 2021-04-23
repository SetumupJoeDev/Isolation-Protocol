using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpenings : MonoBehaviour
{
    public bool             m_openTop;
    public bool             m_openLeft;
    public bool             m_openRight;
    public bool             m_openBottom;
    public LayerMask        m_wallLayer;
    public RoomSpawner[]    m_spawners;
    [HideInInspector]
    public RoomSpawner      m_spawnedFrom;

    protected bool          m_wallAbove;
    protected bool          m_wallBelow;
    protected bool          m_wallRight;
    protected bool          m_wallLeft;

    // Start is called before the first frame update
    protected void Start()
    {
        for (int i = 0; i < m_spawners.Length; i++)
        {
            switch (m_spawners[i].m_doorDirection)
            {
                case Enums.Directions.Top:
                    m_openTop = true;
                    break;
                case Enums.Directions.Left:
                    m_openLeft = true;
                    break;
                case Enums.Directions.Right:
                    m_openRight = true;
                    break;
                case Enums.Directions.Bottom:
                    m_openBottom = true;
                    break;
                default:
                    break;
            }
        }

        switch (m_spawnedFrom.m_doorDirection)
        {
            case Enums.Directions.Top:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openLeft && m_wallLeft ||
                    m_openRight && m_wallRight)
                {
                    m_spawnedFrom.m_spawned = false;
                    m_spawnedFrom.m_levelController.m_numberOfRooms--;
                    m_spawnedFrom.Spawn();
                    Destroy(gameObject);
                }
                break;
            case Enums.Directions.Left:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openLeft && m_wallLeft ||
                    m_openBottom && m_wallBelow)
                {
                    m_spawnedFrom.m_spawned = false;
                    m_spawnedFrom.m_levelController.m_numberOfRooms--;
                    m_spawnedFrom.Spawn();
                    Destroy(gameObject);
                }
                break;
            case Enums.Directions.Right:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openRight && m_wallRight ||
                    m_openBottom && m_wallBelow)
                {
                    m_spawnedFrom.m_spawned = false;
                    m_spawnedFrom.m_levelController.m_numberOfRooms--;
                    m_spawnedFrom.Spawn();
                    Destroy(gameObject);
                }
                break;
            case Enums.Directions.Bottom:
                CheckSurroudings();
                if (m_openLeft && m_wallLeft ||
                    m_openRight && m_wallRight ||
                    m_openBottom && m_wallBelow)
                {
                    m_spawnedFrom.m_spawned = false;
                    m_spawnedFrom.m_levelController.m_numberOfRooms--;
                    m_spawnedFrom.Spawn();
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
        
    }

    protected void CheckSurroudings()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 10f, m_wallLayer))
        {
            m_wallAbove = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, 10f, m_wallLayer))
        {
            m_wallBelow = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, 10f, m_wallLayer))
        {
            m_wallLeft = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, 10f, m_wallLayer))
        {
            m_wallRight = true;
        }
    }
}
