using UnityEngine;

// Template to access room prefabs in an organized form for level generation
public class RoomTemplates : MonoBehaviour
{
    [Header("Basic Door Rooms")]
    public GameObject[] m_topDoorRooms;
    public GameObject[] m_leftDoorRooms;
    public GameObject[] m_rightDoorRooms;
    public GameObject[] m_bottomDoorRooms;

    [Header("Dead End Door Rooms")]
    public GameObject m_topDeadEndRoom;
    public GameObject m_leftDeadEndRoom;
    public GameObject m_rightDeadEndRoom;
    public GameObject m_bottomDeadEndRoom;

    [Header("Exit Rooms")]
    public GameObject m_leftExitRoom;
    public GameObject m_rightExitRoom;
    public GameObject m_bottomExitRoom;

    [Header("Shop Rooms")]
    public GameObject m_topShopRoom;
    public GameObject m_leftShopRoom;
    public GameObject m_rightShopRoom;
    public GameObject m_bottomShopRoom;


    // Idea not yet implemented
    // might be scrapped due to scope

    //[Header("Wall Connection Rooms")]
    //public GameObject[] m_topWallRooms;
    //public GameObject[] m_leftWallRooms;
    //public GameObject[] m_rightWallRooms;
    //public GameObject[] m_bottomWallRooms;

    //[Header("Dead End Wall Connection Rooms")]
    //public GameObject m_topWallRoom;
    //public GameObject m_leftWallRoom;
    //public GameObject m_rightWallRoom;
    //public GameObject m_bottomWallRoom;
}
