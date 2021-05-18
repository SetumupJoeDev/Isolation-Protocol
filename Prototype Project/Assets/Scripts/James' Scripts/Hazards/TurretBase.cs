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
        //Sets the starting rotation of the turret's gun to be half way between the maximum and minimum rotations
        m_turretGun.transform.localEulerAngles = new Vector3( 0 , 0 , m_maxRotation - m_minRotation );

        //Sets the aiming direction of the turret to be the upward transform of the gun
        m_aimingDirection = m_turretGun.transform.up;

    }

    // Update is called once per frame
    void Update()
    {
        //Switches through the different states and acts accordingly
        switch( m_currentTurretState )
        {
            //If the turret is in the searching mode, it searches for any targets
            case ( turretStates.searching ):
                {
                    SearchForTargets( );
                }
                break;
                //If the target is in attack mode, it attempts to fire on the player
            case ( turretStates.attacking ):
                {
                    //Aims the turret towards the target
                    AimAtTarget( );
                    //If the turret isn't firing and hasn't lost sight of the player, the firing coroutine is started
                    if( !m_isFiring && !m_hasLostPlayer )
                    {
                        StartCoroutine( FireAtTarget( ) );
                    }
                    //If the FOV loses sight of the player, the turret begins to check if the player has been lost
                    else if( m_fieldOfView.DrawFOV( m_targetLayer , true ) == null )
                    {
                        m_hasLostPlayer = true;
                        StopCoroutine( FireAtTarget( ) );
                        StartCoroutine( CheckIsTargetLost( ) );
                    }
                }
                break;
        }
    }

    public virtual IEnumerator FireAtTarget( )
    {
        //Waits for the duraction of the fire interval
        yield return new WaitForSeconds( m_fireInterval );

    }

    public virtual IEnumerator CheckIsTargetLost( )
    {
        //Waits for the duration of the time until target lost before setting the current target as null and returning to search mode
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
        //Ping pongs the rotation of the turret gun between the maximum and minimum rotations using the rotation speed
        m_turretGun.transform.localEulerAngles = new Vector3( 0 , 0 , Mathf.PingPong( Time.time * m_rotationSpeed , m_maxRotation ) - m_minRotation );

        //Sets the aiming direction as the upward transform of the turret gun
        m_aimingDirection = m_turretGun.transform.up;

        //Passes the aiming direction of the turret into the FOV
        m_fieldOfView.SetAimDirection( m_aimingDirection );

        //Sets the current target of the turret to whatever the FOV returns
        m_currentTarget = m_fieldOfView.DrawFOV( m_targetLayer , true );

        //If it doesn't return as null, the turret enters attacking mode and the alert sound is played
        if ( m_currentTarget != null )
        {
            m_currentTurretState = turretStates.attacking;
            m_hasLostPlayer = false;
            m_targetFoundSound.Play( );
        }

    }

}
