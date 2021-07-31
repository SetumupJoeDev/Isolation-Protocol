using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls a room's functionality
public class RoomController : MonoBehaviour
{
    [Header("Room Spawning")]
    
    [Tooltip("The room spawn points")]
    public RoomSpawner[]    m_spawners;

    [Tooltip("The spawner that spawned this room")]
    public RoomSpawner      m_spawnedFrom;

    [Tooltip("The layer for the walls")]
    [SerializeField]
    private LayerMask       m_wallLayer;

    [Header("Status")]

    [Tooltip("Whether or not the player has come into this room")]
    public bool             m_discovered;

    [Tooltip("Whether or not all the enemies spawned in this room have been killed")]
    public bool             m_cleared;

    [Tooltip("All doors within this room")]
    [SerializeField]
    private Door[]          m_doors;

    [Header("Loot")]

    [Tooltip("The possible loot that this room can drop when it is cleared")]
    [SerializeField]      
    private GameObject[]    m_lootDrops;

    [Tooltip("How likely it is that loot will be dropped (0 to 1)")]
    [SerializeField]      
    private float           m_dropChance;

    // Booleans to check for the openings of the room and whether or not those openings lead to a wall
    private bool            m_openTop;
    private bool            m_openLeft;
    private bool            m_openRight;
    private bool            m_openBottom;
    private bool            m_wallAbove;
    private bool            m_wallBelow;
    private bool            m_wallRight;
    private bool            m_wallLeft;

    // The distance of the room spawn points to the center of the room
    private float           m_spawnerDistance;

    // The posistions from which to project the raycasts from
    private Vector3[]       m_raycastPositions;

    // The enemy spawner of this room
    [HideInInspector]
    public EnemySpawner     m_enemySpawner;

    // Start is called before the first frame update
    private void Start()
    {
        m_raycastPositions = new Vector3[]
        {
            new Vector3(0,5,0),     // up
            new Vector3(0,-5,0),    // down
            new Vector3(5,0,0),     // right
            new Vector3(-5,0,0)     // left

        };

        // Determines what directions the room has doors in
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

        // Gets spawn point distance from center
        m_spawnerDistance = m_spawners[0].transform.localPosition.magnitude;

        // Changes the room that has been spawned if there are doors leading into other room's walls
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

    // Opens the doors and maybe drops loot when the room is cleared 
    public void Cleared()
    {
        m_cleared = true;
        for (int i = 0; i < m_doors.Length; i++)
        {
            m_doors[i].Open();
        }
        
        float random = Random.Range(0.0f, 1.0f);
        if (random <= m_dropChance)
        {
            DropLoot();
        }
    }

    // Uses raycasts to check the surroundings of the room for walls 
    private void CheckSurroudings()
    {
        if (Physics2D.Raycast(transform.position + m_raycastPositions[0], Vector2.up, m_spawnerDistance, m_wallLayer))
        {
            m_wallAbove = true;
        }
        if (Physics2D.Raycast(transform.position + m_raycastPositions[1], Vector2.down, m_spawnerDistance, m_wallLayer))
        {
            m_wallBelow = true;
        }
        if (Physics2D.Raycast(transform.position + m_raycastPositions[2], Vector2.left, m_spawnerDistance, m_wallLayer))
        {
            m_wallLeft = true;
        }
        if (Physics2D.Raycast(transform.position + m_raycastPositions[3], Vector2.right, m_spawnerDistance, m_wallLayer))
        {
            m_wallRight = true;
        }
    }

    // Access spawner spawned from to spawn another room and destroys itself
    private void SwitchSpawnedRoom()
    {
        m_spawnedFrom.m_spawned = false;
        m_spawnedFrom.m_levelController.m_numberOfRooms--;
        m_spawnedFrom.m_levelController.m_roomList.Remove(gameObject);
        m_spawnedFrom.Spawn();
        Destroy(gameObject);
    }

    // Spawns a random loot object from the lootDrops pool
    private void DropLoot()
    {
        int random = Random.Range(0, m_lootDrops.Length);
        Instantiate(m_lootDrops[random], transform.position, Quaternion.identity);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_discovered == true && collision.gameObject.CompareTag("Enemy"))
        {
            gameEvents.hello.runEnemyTargetPlayer(); // enemies can now chase the player only if the player is within the same room
            Debug.Log("goodbye");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Closes the doors and spawns enemies when the player enters the room for the first time
        if (collision.CompareTag("Player") && !m_discovered)
        {
            m_discovered = true;
            for (int i = 0; i < m_doors.Length; i++)
            {
                m_doors[i].Close();
            }
        }
    }
}
