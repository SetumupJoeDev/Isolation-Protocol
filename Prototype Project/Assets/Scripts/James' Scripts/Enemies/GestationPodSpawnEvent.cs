using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewSpawnEvent" , menuName = "ScriptableObjects/Enemies/GestationPod/SpawnEvent" , order = 1 )]
public class GestationPodSpawnEvent : ScriptableObject
{

    [Tooltip("The prefab of the enemy that this spawn event will create.")]
    public GameObject m_enemyPrefab;

    [Tooltip("The number of enemies that this spawn event will spawn.")]
    public int m_numToSpawn;

    [Tooltip("The maximum distance from the gestation pod that enemies will spawn.")]
    public float m_maxSpawnDistance;

    [Tooltip("The position of the gestation pod this script is attached to.")]
    public Vector3 m_eggPos;

    public void SpawnEvent(  )
    {
        //Loops through the number of enemies to spawn and spawns enemies
        for(int i = 0; i < m_numToSpawn; i++ )
        {
            //Generates a random Vector3 position to spawn the new enemy at using the max spawn distance
            float randX = Random.Range( -m_maxSpawnDistance, m_maxSpawnDistance );
            float randY = Random.Range( -m_maxSpawnDistance, m_maxSpawnDistance );

            Vector3 spawnPos = new Vector3( m_eggPos.x + randX, m_eggPos.y + randY );

            //Instantiates a new enemy at the newly generated Vector3 position
            Instantiate( m_enemyPrefab , spawnPos , Quaternion.identity );

        }
    }

}
