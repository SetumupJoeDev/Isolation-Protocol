using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject m_closedDoor;
    [SerializeField]
    private GameObject m_openDoor;

    public void Close()
    {
        m_openDoor.SetActive(false);
        m_closedDoor.SetActive(true);
    }

    public void Open()
    {
        m_closedDoor.SetActive(false);
        m_openDoor.SetActive(true);
    }
}
