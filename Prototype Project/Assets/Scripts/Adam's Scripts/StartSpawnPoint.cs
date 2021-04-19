using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawnPoint : RoomSpawner
{
    protected override void Spawn()
    {
        m_spawnedRoom = Instantiate(m_templates.m_leftRooms[m_templates.m_leftRooms.Length-1], transform.position, Quaternion.identity);
        m_spawnedRoom.GetComponent<RoomPositionAjustment>().m_directionSpawnedFrom = Enums.Directions.Left;
        m_spawned = true;
    }
}
