using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField]
    protected Enums.Directions doorDirection;
    protected RoomTemplates templates;
    protected int random;
    protected bool spawned;
    protected Vector3 centerDistance;
    protected GameObject spawnedRoom;

    protected virtual void Start()
    {
        spawned = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        centerDistance = transform.localPosition;
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
                    break;
                case Enums.Directions.Right:
                    random = Random.Range(0, templates.leftRooms.Length);
                    spawnedRoom = Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);
                    break;
                case Enums.Directions.Top:
                    random = Random.Range(0, templates.bottomRooms.Length);
                    spawnedRoom = Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
                    break;
                case Enums.Directions.Bottom:
                    random = Random.Range(0, templates.topRooms.Length);
                    spawnedRoom = Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
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
               /* switch (doorDirection)
                {
                    case DoorDirection.Left:
                        switch (collision.GetComponent<RoomSpawner>().doorDirection)
                        {
                            case DoorDirection.Right:
                                Instantiate(templates.rightRooms[2], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Top:
                                Instantiate(templates.rightRooms[1], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Bottom:
                                Instantiate(templates.rightRooms[3], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case DoorDirection.Right:
                        switch (collision.GetComponent<RoomSpawner>().doorDirection)
                        {
                            case DoorDirection.Left:
                                Instantiate(templates.leftRooms[2], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Top:
                                Instantiate(templates.leftRooms[1], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Bottom:
                                Instantiate(templates.leftRooms[3], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case DoorDirection.Top:
                        switch (collision.GetComponent<RoomSpawner>().doorDirection)
                        {
                            case DoorDirection.Left:
                                Instantiate(templates.bottomRooms[3], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Right:
                                Instantiate(templates.bottomRooms[2], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Bottom:
                                Instantiate(templates.bottomRooms[1], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                    case DoorDirection.Bottom:
                        switch (collision.GetComponent<RoomSpawner>().doorDirection)
                        {
                            case DoorDirection.Left:
                                Instantiate(templates.topRooms[2], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Right:
                                Instantiate(templates.topRooms[1], transform.position, Quaternion.identity);
                                break;
                            case DoorDirection.Top:
                                Instantiate(templates.topRooms[3], transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                }*/

                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}
