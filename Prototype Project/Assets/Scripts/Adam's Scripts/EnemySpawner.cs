using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    protected int           m_minSpawns;
    [SerializeField]
    protected int           m_maxSpawns;
    [SerializeField]
    protected GameObject[]  m_spawnPoints;
    [SerializeField]
    protected GameObject[]  m_enemyTypes;
    public List<GameObject> m_spawnedEnemies;
    public bool             m_cleared;

    protected int m_randomEnemy;
    protected int m_numberOfEnemies;
    protected int m_numberOfDeadEnemies;

    // Start is called before the first frame update
    void Start()
    {
        m_numberOfEnemies = Random.Range(m_minSpawns, m_maxSpawns);
        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            Spawn(m_spawnPoints[i].transform.position);
        }
    }

    protected void Update()
    {
        for (int i = 0; i < m_spawnedEnemies.Count; i++)
        {
            if (m_spawnedEnemies[i] == null)
            {
                // IncreaseDead() 
                m_numberOfDeadEnemies++;
                m_spawnedEnemies.RemoveAt(i);
            }
        }
         
        // Include in IncreaseDead()
        if(m_numberOfDeadEnemies == m_numberOfEnemies)
        {
            m_cleared = true;
            // TODO: Make reference to spawner in EnemyBase and call IncreaseDead()
            // TODO: Spawn health/ammo upon cleared
        }
    }

    protected void Spawn(Vector2 position)
    {
        m_randomEnemy = Random.Range(0, m_enemyTypes.Length);
        m_spawnedEnemies.Add(Instantiate(m_enemyTypes[m_randomEnemy], position, Quaternion.identity));
    }
}
