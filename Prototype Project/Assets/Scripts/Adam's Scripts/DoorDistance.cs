using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDistance : MonoBehaviour
{
    [HideInInspector]
    public Enums.Directions directionSpawnedFrom;
    public GameObject[] spawnPoints;
    protected Vector3 ajustedPosition;

    [HideInInspector]
    public Vector3 topDoorDistance;
    [HideInInspector]
    public Vector3 leftDoorDistance;
    [HideInInspector]
    public Vector3 rightDoorDistance;
    [HideInInspector]
    public Vector3 bottomDoorDistance;

    protected void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            switch (spawnPoints[i].GetComponent<RoomSpawner>().doorDirection)
            {
                case Enums.Directions.Top:
                    topDoorDistance = spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Left:
                    leftDoorDistance = spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Right:
                    rightDoorDistance = spawnPoints[i].transform.localPosition;
                    break;
                case Enums.Directions.Bottom:
                    bottomDoorDistance = spawnPoints[i].transform.localPosition;
                    break;
                default:
                    break;
            }
        }

        switch (directionSpawnedFrom)
        {
            case Enums.Directions.Top:
                ajustedPosition = transform.position - topDoorDistance;
                transform.position = ajustedPosition;
                break;
            case Enums.Directions.Left:
                ajustedPosition = transform.position - leftDoorDistance;
                transform.position = ajustedPosition;
                break;
            case Enums.Directions.Right:
                ajustedPosition = transform.position - rightDoorDistance;
                transform.position = ajustedPosition;
                break;
            case Enums.Directions.Bottom:
                ajustedPosition = transform.position - bottomDoorDistance;
                transform.position = ajustedPosition;
                break;
            default:
                Debug.Log("Went to default");
                break;
        }
    }
}
