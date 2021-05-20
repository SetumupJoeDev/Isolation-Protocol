using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls level generation
public class LevelController : MonoBehaviour
{
    [Header("Controls")]

    [Tooltip("The number of this level")]
    public int              m_levelNumber;
    
    [Tooltip("The approximate maximum number of combat rooms to be spawned")]
    public int              m_aproxRoomLimit;

    [Header("Status")]

    [Tooltip("The number of rooms that have been spawned")]
    public int              m_numberOfRooms;

    [Tooltip("Whether or not the limit of rooms has been reached")]
    public bool             m_reachedLimit;

    [Tooltip("Whether or not a shop room has been spawned")]
    public bool             m_hasShop;

    [Tooltip("Whether or not an exit room has been spawned")]
    public bool             m_hasExitRoom;

    [Tooltip("A list of the rooms that have been spawned")]
    public List<GameObject> m_roomList;

    private void Start()
    {
        m_reachedLimit = false;
    }

    private void Update()
    {
        // Checks if the room limit has been reached
        if(m_numberOfRooms >= m_aproxRoomLimit)
        {
            m_reachedLimit = true;
        }
    }
}
