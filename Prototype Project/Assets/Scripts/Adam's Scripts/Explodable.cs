using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    [Tooltip("Damage to be applied to whatever is hit")]
    [SerializeField]
    protected int            m_damage;
    
    [Tooltip("Whether or not it should explode when hit by a projectile")]
    [SerializeField]
    protected bool           m_explodeWhenShot;
    
    [Tooltip("Whether or not it should explode on a timer")]
    [SerializeField]
    protected bool           m_timed;
    
    [Tooltip("The amount of time after which it will explode in seconds")]
    [SerializeField]
    protected float          m_timer;
    
    [Tooltip("The radius of the explosion")]
    [SerializeField]
    protected float          m_explosionRadius;
    
    [Tooltip("The explosion effect particle system")]
    [SerializeField]
    protected ParticleSystem m_vfx;


    public AudioClip m_explosionSound;
    protected void Start()
    {
        if (m_timed)
        {
            StartCoroutine(TimedExplodsion());
        }
    }

    protected void Explode()
    {
        AudioSource.PlayClipAtPoint(m_explosionSound, transform.position);
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