using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [Header("Door Rooms")]
    public GameObject[] m_topDoorRooms;
    public GameObject[] m_leftDoorRooms;
    public GameObject[] m_rightDoorRooms;
    public GameObject[] m_bottomDoorRooms;

    [Header("Dead End Door Rooms")]
    public GameObject m_topDoorRoom;
    public GameObject m_leftDoorRoom;
    public GameObject m_rightDoorRoom;
    public GameObject m_bottomDoorRoom;

    [Header("End Rooms")]
    public GameObject m_topEndRoom;
    public GameObject m_leftEndRoom;
    public GameObject m_rightEndRoom;
    public GameObject m_bottomEndRoom;

    [Header("Shop Rooms")]
    public GameObject m_topShopRoom;
    public GameObject m_leftShopRoom;
    public GameObject m_rightShopRoom;
    public GameObject m_bottomShopRoom;

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
