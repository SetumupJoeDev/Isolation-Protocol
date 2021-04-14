using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    protected enum DoorDirection {Left, Right, Top, Bottom};
    [SerializeField]
    protected DoorDirection doorDirection;

    private RoomTemplates templates;
    private int random;
    private bool spawned;

    protected void Start()
    {
        spawned = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    protected void Spawn()
    {
        if (spawned == false)
        {
            switch (doorDirection)
            {
                case DoorDirection.Left:
                    random = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[random], transform.position, templates.rightRooms[random].transform.rotation);
                    break;
                case DoorDirection.Right:
                    random = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[random], transform.position, templates.leftRooms[random].transform.rotation);
                    break;
                case DoorDirection.Top:
                    random = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[random], transform.position, templates.bottomRooms[random].transform.rotation);
                    break;
                case DoorDirection.Bottom:
                    random = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[random], transform.position, templates.topRooms[random].transform.rotation);
                    break;
            }

            spawned = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            //if(collision.GetComponent<RoomSpawner>().spawned == false && !spawned)
            //{
            //    Instantiate
            //}
            Destroy(gameObject);
        }
        spawned = true;
    }
}
