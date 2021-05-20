using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns random enemies in a room depending on spawn points and enemy pool
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    
    [Tooltip("The minimum number of enemies to spawn")]
    [SerializeField]
    private int             m_minSpawns;

    [Tooltip("The maximum number of enemies to spawn")]
    [SerializeField]
    private int             m_maxSpawns;

    [Tooltip("The positions at which to spawn the enemies")]
    [SerializeField]
    private GameObject[]    m_spawnPoints;

    [Tooltip("The different enemies to be spawned")]
    [SerializeField]
    private GameObject[]    m_enemyTypes;

    [Tooltip("A list of the enemies that have been spawned")]
    public List<GameObject> m_spawnedEnemies;

    // A random value to pick an enemy type out of the enemy pool
    private int             m_randomEnemy;

    // A random number of enemies to be spawned
    private int             m_numberOfEnemies;

    // The number of enemies that havev been killed
    private int             m_numberOfDeadEnemies;

    // The room the spawner is attached to
    private RoomController  m_parentRoom;

    // Start is called before the first frame update
    private void Start()
    {
        m_parentRoom = GetComponentInParent<RoomController>();
        m_parentRoom.m_enemySpawner = this;
    }

    // Increases number of dead enemies and removes them from the list
    public void IncreaseDead(GameObject spawnedEnemy)
    {
        m_numberOfDeadEnemies++;
        m_spawnedEnemies.Remove(spawnedEnemy);

        // Checks if all enemies are dead and marks the room as cleared
        if(m_numberOfDeadEnemies == m_numberOfEnemies)
        {
            m_parentRoom.Cleared();
        }
    }

    // Spawns the enemies
    public void SpawnEnemies()
    {
        // Assigns a random value within range to number of enemies
        m_numberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns + 1);

        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            // Picks a random enemy type from enemy pool
            m_randomEnemy = Random.Range(0, m_enemyTypes.Length);
            
            // Spawns enemy at target position
            GameObject spawnedEnemy = Instantiate(m_enemyTypes[m_randomEnemy], m_spawnPoints[i].transform.position, Quaternion.identity);

            // Assigns enemy to this spawner 
            spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;

            // Attaches enemy to the room it was spawned in
            spawnedEnemy.transform.parent = m_parentRoom.transform;

            // Adds enemy to list
            m_spawnedEnemies.Add(spawnedEnemy);
        }
    }
}
