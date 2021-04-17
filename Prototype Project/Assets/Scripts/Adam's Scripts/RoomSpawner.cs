using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Enums.Directions doorDirection;
    
    protected RoomTemplates templates;
    protected int random;
    protected bool spawned;
    protected GameObject spawnedRoom;

    protected virtual void Start()
    {
        spawned = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    protected virtual void Spawn()
    {
        if (spawned == false)
        {
            switch (doorDirection)
            {
                case Enums.Directions.Left:
                    random = Random.Range(0, templates.rightRooms.Length);
                    spawnedRoom = Instantiate(templates.rightRooms[random], transform.position, Quaternion.identity);
                    spawnedRoom.GetComponent<DoorDistance>().directionSpawnedFrom = Enums.Directions.Right;
                    break;
                case Enums.Directions.Right:
                    random = Random.Range(0, templates.leftRooms.Length);
                    spawnedRoom = Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);
                    spawnedRoom.GetComponent<DoorDistance>().directionSpawnedFrom = Enums.Directions.Left;
                    break;
                case Enums.Directions.Top:
                    random = Random.Range(0, templates.bottomRooms.Length);
                    spawnedRoom = Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
                    spawnedRoom.GetComponent<DoorDistance>().directionSpawnedFrom = Enums.Directions.Bottom;
                    break;
                case Enums.Directions.Bottom:
                    random = Random.Range(0, templates.topRooms.Length);
                    spawnedRoom = Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
                    spawnedRoom.GetComponent<DoorDistance>().directionSpawnedFrom = Enums.Directions.Top;
                    break;
            }

            spawned = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>().spawned == false && !spawned)
            {
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}
