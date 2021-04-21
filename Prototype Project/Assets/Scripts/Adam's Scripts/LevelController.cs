using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int  m_roomLimit;
    public int  m_numberOfRooms;
    public bool m_reachedLimit;

    protected void Start()
    {
        m_reachedLimit = false;
    }

    protected void Update()
    {
        if(m_numberOfRooms >= m_roomLimit)
        {
            m_reachedLimit = true;
        }
    }
}
