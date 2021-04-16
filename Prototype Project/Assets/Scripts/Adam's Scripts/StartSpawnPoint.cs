using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawnPoint : RoomSpawner
{
    protected override void Spawn()
    {
        spawnedRoom = Instantiate(templates.leftRooms[templates.leftRooms.Length-1], transform.position, Quaternion.identity);
        spawnedRoom.GetComponent<DoorDistance>().directionSpawnedFrom = Enums.Directions.Left;
        spawned = true;
    }
}
