using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SentryMode : DroneBehaviourBase
{

    #region Behaviour Parameters

    #region Cooldown

    [Header("Cooldown")]

    [SerializeField]
    private bool m_isInCooldown;

    [SerializeField]
    private float m_cooldownDuration;

    [SerializeField]
    private Image m_cooldownOverlay;

    #endregion

    #region Combat

    [Header("Combat")]

    [SerializeField]
    private bool m_isInSentryMode;

    [SerializeField]
    private bool m_isFiring;

    [SerializeField]
    private float m_fireRadius;

    [SerializeField]
    private float m_fireInterval;

    [SerializeField]
    private GameObject m_projectilePrefab;

    [SerializeField]
    private LayerMask m_targetLayer;

    private GameObject m_currentEnemy;

    private Vector3 m_aimingDirection;

    [SerializeField]
    private float m_sentryModeDuration;

    #endregion

    #endregion

    #region Sounds

    [Header("Sounds")]

    [SerializeField]
    private AudioClip m_setupSound;

    [SerializeField]
    private AudioClip m_deactivatedSound;

    [SerializeField]
    private AudioClip m_firingSound;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Update( )
    {
        if ( Input.GetKeyDown( KeyCode.Q ) )
        {
            if ( !m_isInSentryMode && !m_isInCooldown )
            {
                ActivateEffect( );
            }
            else if( m_isInSentryMode )
            {

                DisableModuleBehaviour( );

                StartCoroutine( SentryModeCooldown( ) );

            }
        }
        if ( !m_isFiring && m_isInSentryMode && m_currentEnemy == null && CheckForTargets( ) )
        {
            StartCoroutine( FireAtEnemy( ) );
        }
        if( m_isInCooldown )
        {
            m_cooldownOverlay.fillAmount -= Time.deltaTime / m_cooldownDuration;
        }
    }

    public IEnumerator FireAtEnemy( )
    {

        m_isFiring = true;

        m_aimingDirection = m_currentEnemy.transform.position - transform.position;

        ProjectileBase newProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBase>( );

        newProjectile.m_projectileVelocity = m_aimingDirection;

        AudioSource.PlayClipAtPoint( m_firingSound , transform.position );

        yield return new WaitForSeconds( m_fireInterval );

        if ( m_currentEnemy == null )
        {
            m_isFiring = false;
        }
        else
        {
            StartCoroutine( FireAtEnemy( ) );
        }

    }

    public bool CheckForTargets( )
    {

        Collider2D newTarget = Physics2D.OverlapCircle(transform.position, m_fireRadius, m_targetLayer);

        if ( newTarget )
        {
            m_currentEnemy = newTarget.gameObject;
            return true;
        }
        else
        {
            return false;
        }

    }

    public override void EnableModuleBehaviour( )
    {
        AudioSource.PlayClipAtPoint( m_setupSound , transform.position );

        m_isInSentryMode = true;

        StartCoroutine( SentryTimer( ) );

    }

    public IEnumerator SentryTimer( )
    {

        yield return new WaitForSeconds( m_sentryModeDuration );

        DisableModuleBehaviour( );

        StartCoroutine( SentryModeCooldown( ) );

    }

    public IEnumerator SentryModeCooldown( )
    {

        m_isInCooldown = true;

        m_cooldownOverlay.fillAmount = 1;

        yield return new WaitForSeconds( m_cooldownDuration );

        m_isInCooldown = false;

        m_cooldownOverlay.fillAmount = 0;

    }

    public override void DisableModuleBehaviour( )
    {
        AudioSource.PlayClipAtPoint( m_deactivatedSound , transform.position );

        m_isFiring = false;

        m_isInSentryMode = false;

        m_droneController.EnableBasicBehaviours( );

        StopAllCoroutines( );

    }

}
