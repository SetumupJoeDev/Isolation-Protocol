using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawnPoint : RoomSpawner
{
    protected override void Spawn()
    {
        m_random = Random.Range(1, m_templates.m_bottomRooms.Length);
        m_spawnedRoom = Instantiate(m_templates.m_bottomRooms[m_random], transform.position, Quaternion.identity);
        m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Bottom;
        m_spawned = true;
        m_levelController.m_numberOfRooms++;
    }
}
