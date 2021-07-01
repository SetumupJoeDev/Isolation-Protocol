﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    [SerializeField]
    protected int            m_damage;
    [SerializeField]
    protected bool           m_explodeWhenShot;
    [SerializeField]
    protected bool           m_timed;
    [SerializeField]
    protected float          m_timer;
    [SerializeField]
    protected float          m_explosionRadius;
    [SerializeField]
    protected ParticleSystem m_vfx;

    protected void Start()
    {
        if (m_timed)
        {
            StartCoroutine(TimedExplodsion());
        }
    }

    protected void Explode()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, m_explosionRadius);
        if (hit.Length != 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].CompareTag("Player") || hit[i].CompareTag("Enemy"))
                {
                    hit[i].gameObject.GetComponent<HealthManager>().TakeDamage(m_damage);
                }
                else if (hit[i].CompareTag("Explosive"))
                {
                    Destroy(gameObject);
                    hit[i].gameObject.GetComponent<Explodable>().Explode();
                }
            }
        }

        m_vfx.transform.SetParent(null);
        m_vfx.Play();
        Destroy(gameObject);
    }

    protected IEnumerator TimedExplodsion()
    {
        yield return new WaitForSeconds(m_timer);
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile") && m_explodeWhenShot)
        {
            Explode();
        }
    }
}