using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns random enemies in a room depending on spawn points and enemy pool
public class EnemySpawner : MonoBehaviour
{
    [Header("Randomization")]
    
    [Tooltip("Whether or not to randomize which enemy types to be spawned")]
    [SerializeField]
    private bool            m_randomizeEnemies;

    [Tooltip("Whether or not to randomize spawning positions")]
    [SerializeField]
    private bool            m_randomizePositions;

    [Tooltip("The minimum number of enemies to spawn")]
    [SerializeField]
    private int             m_minSpawns;

    [Tooltip("The maximum number of enemies to spawn")]
    [SerializeField]
    private int             m_maxSpawns;

    [Header("( Values must total up to 1.0 )")]

    [Tooltip("Chance of spawning a common enemy type (0.0 to 1.0)")]
    [SerializeField]
    private float           m_commonEnemyChance;
    
    [Tooltip("Chance of spawning an uncommon enemy type (0.0 to 1.0)")]
    [SerializeField]
    private float           m_uncommonEnemyChance;

    [Tooltip("Chance of spawning a rare enemy type (0.0 to 1.0)")]
    [SerializeField]
    private float           m_rareEnemyChance;
    

    [Header("Positions")]

    [Tooltip("The positions at which to spawn the enemies")]
    [SerializeField]
    private GameObject[]    m_spawnPoints;

    [Tooltip("The radius of the circle around the spawner in which enemies can spawn randomly")]
    [SerializeField]
    private float           m_randomSpawnRadius;

    [Header("Enemies")]

    [Tooltip("The different enemies that can be spawned")]
    [SerializeField]
    private GameObject[]    m_enemyTypes;

    [Tooltip("Enemy types that will be commonly spawned")]
    [SerializeField]
    private GameObject[]    m_commonEnemyTypes;

    [Tooltip("Enemy types that will be usually spawned")]
    [SerializeField]
    private GameObject[]    m_uncommonEnemyTypes;

    [Tooltip("Enemy types that will be rarely spawned")]
    [SerializeField]
    private GameObject[]    m_rareEnemyTypes;

    [Tooltip("The different enemy layouts that can be spawned")]
    [SerializeField]
    private EnemyLayout[]   m_enemyLayouts;

    [Tooltip("A list of the enemies that have been spawned")]
    public List<GameObject> m_spawnedEnemies;

    // A random value to pick an enemy type out of the enemy pool
    private int             m_randomEnemy;

    //The room the spawner is attached to
    private RoomController  m_parentRoom;

    // Start is called before the first frame update
    private void Start()
    {
        m_parentRoom = GetComponentInParent<RoomController>();
        m_parentRoom.m_enemySpawner = this;

        if (m_randomizeEnemies)
        {
            SpawnRandomEnemies();
        }
        else
        {
            SpawnRandomEnemyLayout();
        }
    }

    // Increases number of dead enemies and removes them from the list
    public void IncreaseDead(GameObject spawnedEnemy)
    {
        m_spawnedEnemies.Remove(spawnedEnemy);
        switch (spawnedEnemy)
        {
            default:
                break;
        }

        // Checks if all enemies are dead and marks the room as cleared
        if(m_spawnedEnemies.Count <= 0)
        {
            m_parentRoom.Cleared();
        }
    }

    public void SpawnRandomEnemyLayout()
    {
        int randomLayout = Random.Range(0, m_enemyLayouts.Length);
        
        if (m_randomizePositions)
        {
            for (int i = 0; i < m_enemyLayouts[randomLayout].m_enemies.Length; i++)
            {
                float randomX = Random.Range(-m_randomSpawnRadius, m_randomSpawnRadius);
                float randomY = Random.Range(-m_randomSpawnRadius, m_randomSpawnRadius);

                Vector3 randomSpawnPosition = new Vector3(transform.position.x + randomX,transform.position.y + randomY, 0);

                // Spawns enemy at target position
                GameObject spawnedEnemy = Instantiate(m_enemyLayouts[randomLayout].m_enemies[i], randomSpawnPosition, Quaternion.identity);

                // Assigns enemy to this spawner 
                spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                // Attaches enemy to the room it was spawned in
                spawnedEnemy.transform.parent = m_parentRoom.transform;

                // Adds enemy to list
                m_spawnedEnemies.Add(spawnedEnemy);
            }
        }
        else
        {
            for (int i = 0; i < m_enemyLayouts[randomLayout].m_enemies.Length; i++)
            {
                Vector3 spawnPosition = transform.position + m_enemyLayouts[randomLayout].m_localPositions[i];

                // Spawns enemy at target position
                GameObject spawnedEnemy = Instantiate(m_enemyLayouts[randomLayout].m_enemies[i], spawnPosition, Quaternion.identity);

                // Assigns enemy to this spawner 
                spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                // Attaches enemy to the room it was spawned in
                spawnedEnemy.transform.parent = m_parentRoom.transform;

                // Adds enemy to list
                m_spawnedEnemies.Add(spawnedEnemy);
            }
        }
    }

    // Spawns a random assortment of enemies within set limitations
    public void SpawnRandomEnemies()
    {
        // Assigns a random value within range to number of enemies
        int numberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns + 1);

        if (m_randomizePositions)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                float randomX = Random.Range(-m_randomSpawnRadius, m_randomSpawnRadius);
                float randomY = Random.Range(-m_randomSpawnRadius, m_randomSpawnRadius);

                Vector3 randomSpawnPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0);

                float randomPercentage = Random.Range(0f, 1f);

                if (randomPercentage <= m_rareEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_rareEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_rareEnemyTypes[m_randomEnemy], randomSpawnPosition, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
                else if (randomPercentage <= m_uncommonEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_uncommonEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_uncommonEnemyTypes[m_randomEnemy], randomSpawnPosition, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
                else if (randomPercentage >= m_commonEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_commonEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_commonEnemyTypes[m_randomEnemy], randomSpawnPosition, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
            }
        }
        else
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                float randomPercentage = Random.Range(0f, 1f);

                if (randomPercentage <= m_rareEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_rareEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_rareEnemyTypes[m_randomEnemy], m_spawnPoints[i].transform.position, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
                else if(randomPercentage <= m_uncommonEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_uncommonEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_uncommonEnemyTypes[m_randomEnemy], m_spawnPoints[i].transform.position, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
                else if(randomPercentage >= m_commonEnemyChance)
                {
                    // Picks a random enemy type from enemy pool
                    m_randomEnemy = Random.Range(0, m_commonEnemyTypes.Length);

                    // Spawns enemy at target position
                    GameObject spawnedEnemy = Instantiate(m_commonEnemyTypes[m_randomEnemy], m_spawnPoints[i].transform.position, Quaternion.identity);

                    // Assigns enemy to this spawner 
                    spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

                    // Attaches enemy to the room it was spawned in
                    spawnedEnemy.transform.parent = m_parentRoom.transform;

                    // Adds enemy to list
                    m_spawnedEnemies.Add(spawnedEnemy);
                }
            }
        }
    }
}
