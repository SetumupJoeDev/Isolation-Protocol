using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpenings : MonoBehaviour
{
    public bool m_openTop;
    public bool m_openLeft;
    public bool m_openRight;
    public bool m_openBottom;

    protected RoomSpawner[] m_spawners;

    // Start is called before the first frame update
    protected void Start()
    {
        m_spawners = new RoomSpawner[gameObject.GetComponentsInChildren<RoomSpawner>().Length + 1];

        for (int i = 0; i < gameObject.GetComponentsInChildren<RoomSpawner>().Length; i++)
        {
           gameObject.GetComponentsInChildren<RoomSpawner>().CopyTo(m_spawners, i);
        }

        for (int i = 0; i < m_spawners.Length; i++)
        {
            switch (m_spawners[i].m_doorDirection)
            {
                case Enums.Directions.Top:
                    m_openTop = true;
                    break;
                case Enums.Directions.Left:
                    m_openLeft = true;
                    break;
                case Enums.Directions.Right:
                    m_openRight = true;
                    break;
                case Enums.Directions.Bottom:
                    m_openBottom = true;
                    break;
                default:
                    break;
            }
        }
    }
}
