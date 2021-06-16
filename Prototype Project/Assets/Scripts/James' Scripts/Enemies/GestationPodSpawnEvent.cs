using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewSpawnEvent" , menuName = "ScriptableObjects/Enemies/GestationPod/SpawnEvent" , order = 1 )]
public class GestationPodSpawnEvent : ScriptableObject
{

    public GameObject m_enemyPrefab;

    public int m_numToSpawn;

    public float m_maxSpawnDistance;

    public Vector3 m_eggPos;

    public void SpawnEvent(  )
    {
        for(int i = 0; i < m_numToSpawn; i++ )
        {
            float randX = Random.Range( -m_maxSpawnDistance, m_maxSpawnDistance );
            float randY = Random.Range( -m_maxSpawnDistance, m_maxSpawnDistance );

            Vector3 spawnPos = new Vector3( m_eggPos.x + randX, m_eggPos.y + randY );

            Instantiate( m_enemyPrefab , spawnPos , Quaternion.identity );

        }
    }

}
