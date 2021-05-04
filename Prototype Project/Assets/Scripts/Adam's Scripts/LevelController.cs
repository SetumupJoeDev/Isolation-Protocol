using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int              m_levelNumber;
    public int              m_aproxRoomLimit;
    public int              m_numberOfRooms;
    public bool             m_reachedLimit;
    public bool             m_hasShop;
    public bool             m_hasEndRoom;
    public List<GameObject> m_roomList;

    private void Start()
    {
        m_reachedLimit = false;
    }

    private void Update()
    {
        if(m_numberOfRooms >= m_aproxRoomLimit - 2)
        {
            m_reachedLimit = true;
        }
    }
}
