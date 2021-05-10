using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    private int             m_minSpawns;
    [SerializeField]
    private int             m_maxSpawns;
    [SerializeField]
    private GameObject[]    m_spawnPoints;
    [SerializeField]
    private GameObject[]    m_enemyTypes;
    public List<GameObject> m_spawnedEnemies;

    private int             m_randomEnemy;
    private int             m_numberOfEnemies;
    private int             m_numberOfDeadEnemies;
    private RoomController  m_parentRoom;

    // Start is called before the first frame update
    void Start()
    {
        m_parentRoom = GetComponentInParent<RoomController>();
        m_parentRoom.m_enemySpawner = this;
    }

    public void IncreaseDead(GameObject spawnedEnemy)
    {
        m_numberOfDeadEnemies++;
        m_spawnedEnemies.Remove(spawnedEnemy);

        if(m_numberOfDeadEnemies == m_numberOfEnemies)
        {
            m_parentRoom.Cleared();
        }
    }

    public void SpawnEnemies()
    {
        m_numberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns + 1);
        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            m_randomEnemy = Random.Range(0, m_enemyTypes.Length);
            GameObject spawnedEnemy = Instantiate(m_enemyTypes[m_randomEnemy], m_spawnPoints[i].transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;
            spawnedEnemy.transform.parent = m_parentRoom.transform;
            m_spawnedEnemies.Add(spawnedEnemy);
        }
    }
}
