using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner2 : RoomSpawner
{
    protected Vector3 spawnPosition;

    protected override void Start()
    {
        base.Start();
        spawnPosition = transform.position;
    }

    protected override void Spawn()
    {
        if (spawned == false)
        {
            switch (doorDirection)
            {
                case DoorDirection.Left:
                    random = Random.Range(0, templates.rightRooms.Length);
                    spawnPosition.x = transform.position.x - templates.rightRooms[random].GetComponent<RoomOriginCalculator>().spawnPointLeftPosition.x;
                    Instantiate(templates.rightRooms[random], spawnPosition, Quaternion.identity);
                    break;
                case DoorDirection.Right:
                    random = Random.Range(0, templates.leftRooms.Length);
                    spawnPosition.x = transform.position.x + templates.leftRooms[random].GetComponent<RoomOriginCalculator>().spawnPointRightPosition.x;
                    Instantiate(templates.leftRooms[random], spawnPosition, Quaternion.identity);
                    break;
                case DoorDirection.Top:
                    random = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
                    break;
                case DoorDirection.Bottom:
                    random = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
                    break;
            }

            spawned = true;
        }
    }
}
