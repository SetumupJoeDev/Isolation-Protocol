using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    protected int m_minSpawns;
    [SerializeField]
    protected int m_maxSpawns;
    [SerializeField]
    protected GameObject[] m_spawnPoints;
    [SerializeField]
    protected GameObject[] m_enemyTypes;

    //protected GameObject[] m_spawnedEnemies;
    protected int m_randomEnemy;
    protected int m_randomNumberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        m_randomNumberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns);
        for (int i = 0; i < m_randomNumberOfEnemies + 1; i++)
        {
            Spawn(m_spawnPoints[i].transform.position);
        }
    }

    protected void Spawn(Vector2 position)
    {
        m_randomEnemy = Random.Range(0, m_enemyTypes.Length);
        Instantiate(m_enemyTypes[m_randomEnemy], position, Quaternion.identity);
    }
}
