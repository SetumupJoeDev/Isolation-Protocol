using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDistance : MonoBehaviour
{
    [SerializeField]
    public Enums.Directions directionSpawnedFrom;
    public GameObject leftSpawnPoint;
    public GameObject rightSpawnPoint;
    public GameObject topSpawnPoint;
    public GameObject bottomSpawnPoint;
    protected Vector3 ajustedPosition;

    [HideInInspector]
    public Vector3 leftDoorDistance;
    [HideInInspector]
    public Vector3 rightDoorDistance;
    [HideInInspector]
    public Vector3 topDoorDistance;
    [HideInInspector]
    public Vector3 bottomDoorDistance;

    protected void Start()
    {
        leftDoorDistance = leftSpawnPoint.transform.localPosition;
        rightDoorDistance = rightSpawnPoint.transform.localPosition;
        topDoorDistance = topSpawnPoint.transform.localPosition;
        bottomDoorDistance = bottomSpawnPoint.transform.localPosition;

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
        }
    }
}
