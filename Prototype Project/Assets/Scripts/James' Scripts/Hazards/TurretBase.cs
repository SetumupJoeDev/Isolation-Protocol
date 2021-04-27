using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{

    #region PlayerDetection

    [Header("Player Detection")]

    public FieldOfView m_fieldOfView;

    public LayerMask m_targetLayer;

    public enum turretStates { searching, attacking, disabled}

    public turretStates m_currentTurretState;

    [Range(0, 360)]
    public int m_minRotation;

    [Range(0, 360)]
    public int m_maxRotation;

    public bool m_rotatingLeft;

    public float m_rotationSpeed;

    public bool m_hasLostPlayer;

    public float m_timeUntilTargetLost;

    #endregion

    #region Aiming & Firing

    [Header("Aiming & Firing")]

    [Tooltip("The gun of the turret that will aim and move.")]
    public GameObject m_turretGun;

    [Tooltip("The current target of the turret.")]
    public GameObject m_currentTarget;

    [Tooltip("The current direction that the turret is aiming in.")]
    public Vector3 m_aimingDirection;

    public AudioSource m_firingSound;

    public AudioSource m_targetFoundSound;

    public float m_fireInterval;

    public bool m_isFiring;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        m_turretGun.transform.localEulerAngles = new Vector3( 0 , 0 , m_maxRotation - m_minRotation );

        m_aimingDirection = m_turretGun.transform.up;

    }

    // Update is called once per frame
    void Update()
    {

        switch( m_currentTurretState )
        {
            case ( turretStates.searching ):
                {
                    SearchForTargets( );
                }
                break;
            case ( turretStates.attacking ):
                {
                    AimAtTarget( );
                    if( !m_isFiring && !m_hasLostPlayer )
                    {
                        StartCoroutine( FireAtTarget( ) );
                    }
                    if( m_fieldOfView.DrawFOV( m_targetLayer , true ) == null )
                    {
                        m_hasLostPlayer = true;
                        StartCoroutine( CheckIsTargetLost( ) );
                    }
                    else if ( m_hasLostPlayer )
                    {
                        StopCoroutine( CheckIsTargetLost( ) );
                    }
                }
                break;
        }

        m_fieldOfView.SetOrigin( transform.position );

    }

    public virtual IEnumerator FireAtTarget( )
    {

        yield return new WaitForSeconds( m_fireInterval );

    }

    public virtual IEnumerator CheckIsTargetLost( )
    {
        yield return new WaitForSeconds( m_timeUntilTargetLost );

        m_currentTarget = null;

        m_currentTurretState = turretStates.searching;

    }

    protected virtual void AimAtTarget( )
    {
        //Calculates the direction for the weapon to point based on the positions of the mouse and the object
        m_aimingDirection = m_currentTarget.transform.position - m_turretGun.transform.position;

        m_fieldOfView.SetAimDirection( m_aimingDirection );

        //Calculates the angle using the x and y of the direction vector, converting it to degrees for use in a quaternion
        float angle = Mathf.Atan2( m_aimingDirection.x, m_aimingDirection.y ) * Mathf.Rad2Deg;

        //Calculates the rotation around the Z axis using the angle
        Quaternion rotation = Quaternion.AngleAxis( angle, Vector3.back );

        //Sets the rotation of the weapon to the rotation calculated above
        m_turretGun.transform.rotation = rotation;

    }

    public void SearchForTargets( )
    {

        m_turretGun.transform.localEulerAngles = new Vector3( 0 , 0 , Mathf.PingPong( Time.time * m_rotationSpeed , m_maxRotation ) - m_minRotation );

        m_aimingDirection = m_turretGun.transform.up;

        m_fieldOfView.SetAimDirection( m_aimingDirection );

        m_currentTarget = m_fieldOfView.DrawFOV( m_targetLayer , true );
        if ( m_currentTarget != null )
        {
            m_currentTurretState = turretStates.attacking;
            m_hasLostPlayer = false;
            m_targetFoundSound.Play( );
        }

    }

}
