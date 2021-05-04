using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    protected int           m_minSpawns;
    [SerializeField]
    protected int           m_maxSpawns;
    [SerializeField]
    protected GameObject[]  m_spawnPoints;
    [SerializeField]
    protected GameObject[]  m_enemyTypes;
    public List<GameObject> m_spawnedEnemies;

    [Header("Clearing")]
    public bool             m_cleared;
    [SerializeField]
    protected GameObject[]  m_lootDrops;
    public float            m_dropChance;

    protected int           m_randomEnemy;
    protected int           m_numberOfEnemies;
    protected int           m_numberOfDeadEnemies;

    // Start is called before the first frame update
    void Start()
    {
        m_numberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns);
        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            Spawn(m_spawnPoints[i].transform.position);
        }
    }

    public void IncreaseDead(GameObject spawnedEnemy)
    {
        m_numberOfDeadEnemies++;
        m_spawnedEnemies.Remove(spawnedEnemy);

        if(m_numberOfDeadEnemies == m_numberOfEnemies)
        {
            float random = Random.Range(0.0f, 1.0f);
            if(random <= m_dropChance)
            {
                DropLoot();
            }
        }
    }

    protected void DropLoot()
    {
        int random = Random.Range(0, m_lootDrops.Length);
        Instantiate(m_lootDrops[random], transform.position, Quaternion.identity);
    }

    protected void Spawn(Vector2 position)
    {
        m_randomEnemy = Random.Range(0, m_enemyTypes.Length);
        GameObject spawnedEnemy = Instantiate(m_enemyTypes[m_randomEnemy], position, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyBase>().m_spawner = this;
        m_spawnedEnemies.Add(spawnedEnemy);
    }
}
