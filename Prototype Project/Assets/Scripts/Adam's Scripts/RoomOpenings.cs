using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpenings : MonoBehaviour
{
    public RoomSpawner[]    m_spawners;
    public RoomSpawner      m_spawnedFrom;
    
    [SerializeField]
    private LayerMask     m_wallLayer;

    private bool          m_openTop;
    private bool          m_openLeft;
    private bool          m_openRight;
    private bool          m_openBottom;
    private bool          m_wallAbove;
    private bool          m_wallBelow;
    private bool          m_wallRight;
    private bool          m_wallLeft;
    private float         m_spawnerDistance;

    // Start is called before the first frame update
    private void Start()
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

        m_spawnerDistance = m_spawners[0].transform.localPosition.magnitude;

        switch (m_spawnedFrom.m_doorDirection)
        {
            case Enums.Directions.Top:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openLeft && m_wallLeft ||
                    m_openRight && m_wallRight)
                {
                    SwitchSpawnedRoom();
                }
                break;
            case Enums.Directions.Left:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openLeft && m_wallLeft ||
                    m_openBottom && m_wallBelow)
                {
                    SwitchSpawnedRoom();
                }
                break;
            case Enums.Directions.Right:
                CheckSurroudings();
                if (m_openTop && m_wallAbove ||
                    m_openRight && m_wallRight ||
                    m_openBottom && m_wallBelow)
                {
                    SwitchSpawnedRoom();
                }
                break;
            case Enums.Directions.Bottom:
                CheckSurroudings();
                if (m_openLeft && m_wallLeft ||
                    m_openRight && m_wallRight ||
                    m_openBottom && m_wallBelow)
                {
                    SwitchSpawnedRoom();
                }
                break;
            default:
                break;
        }
        
    }

    private void CheckSurroudings()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, m_spawnerDistance, m_wallLayer))
        {
            m_wallAbove = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, m_spawnerDistance, m_wallLayer))
        {
            m_wallBelow = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, m_spawnerDistance, m_wallLayer))
        {
            m_wallLeft = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, m_spawnerDistance, m_wallLayer))
        {
            m_wallRight = true;
        }
    }

    private void SwitchSpawnedRoom()
    {
        m_spawnedFrom.m_spawned = false;
        m_spawnedFrom.m_levelController.m_numberOfRooms--;
        m_spawnedFrom.m_levelController.m_roomList.Remove(gameObject);
        m_spawnedFrom.Spawn();
        Destroy(gameObject);
    }
}
