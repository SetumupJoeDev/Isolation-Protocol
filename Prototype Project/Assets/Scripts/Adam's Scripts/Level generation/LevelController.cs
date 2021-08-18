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
    public int              m_numberOfCombatRooms;

    [Tooltip("Whether or not the limit of rooms has been reached")]
    public bool             m_reachedLimit;

    [Tooltip("The amount of rooms that have been cleared")]
    public int              m_roomsCleared;

    [Tooltip("The door to the exit room")]
    public Door             m_exitRoomDoor;

    [Tooltip("A list of the rooms that have been spawned")]
    public List<GameObject> m_roomList;

    private void Start()
    {
        m_reachedLimit = false;
    }

    private void Update()
    {
        // Checks if the room limit has been reached
        if(m_numberOfCombatRooms >= m_aproxRoomLimit && !m_reachedLimit)
        {
            m_reachedLimit = true;

            gameEvents.hello.levelLoaded( );
        }

        if(m_roomsCleared >= m_numberOfCombatRooms && m_exitRoomDoor.m_closed && m_reachedLimit)
        {
            m_exitRoomDoor.Open();
        }
    }
}
