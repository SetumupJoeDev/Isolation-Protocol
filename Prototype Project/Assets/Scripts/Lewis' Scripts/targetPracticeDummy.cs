using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPracticeDummy : MonoBehaviour
{

    public AudioSource m_hitSound;

    private Vector3 startPos;

    public float freq = 5f;

    public float magnitude = 5f;

    public float offset = 0f;

    private void Start()
    {
        startPos = gameObject.transform.position;
    }
    void FixedUpdate()
    {

transform.position = startPos + transform.up * Mathf.Sin(Time.time * freq + offset) * magnitude;
          
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileBase>() != null)
        {
           
            m_hitSound.Play();
            TutorialEventListener.current.DummyHit();
        }
    }


}
