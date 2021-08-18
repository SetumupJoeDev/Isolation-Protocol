using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Open and Closes doors
public class Door : MonoBehaviour
{
    [Tooltip("Whether or not the door is closed")]
    public bool         m_closed;

    [Tooltip("The closed door tilemap object")]
    [SerializeField]
    private GameObject  m_closedDoor;

    [Tooltip("The open door tilemap object")]
    [SerializeField]
    private GameObject  m_openDoor;

    public AudioClip    m_doorOpenNoise;
    public AudioClip    m_doorCloseNoise;

    private void Start()
    {
        //if(m_openDoor.activeSelf && !m_closedDoor.activeSelf)
        //{
        //    m_closed = false;
        //}
        //else if(m_closedDoor.activeSelf && !m_openDoor.activeSelf)
        //{
        //    m_closed = true;
        //}
    }

    // Switches active tilemap to closed door
    public void Close()
    {
        AudioSource.PlayClipAtPoint(m_doorCloseNoise, transform.position);
        m_openDoor.SetActive(false);
        m_closedDoor.SetActive(true);
        m_closed = true;
    }

    // Switches active tilemap to open door
    public void Open()
    {
        AudioSource.PlayClipAtPoint(m_doorOpenNoise, transform.position);
        m_closedDoor.SetActive(false);
        m_openDoor.SetActive(true);
        m_closed = false;
    }
}
