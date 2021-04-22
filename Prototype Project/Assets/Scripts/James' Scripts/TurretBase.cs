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

    public int m_minRotation;

    public int m_maxRotation;

    public bool m_rotatingLeft;

    public float m_rotationSpeed;

    #endregion

    #region Aiming & Firing

    [Header("Aiming & Firing")]

    [Tooltip("The gun of the turret that will aim and move.")]
    public GameObject m_turretGun;

    [Tooltip("The current target of the turret.")]
    public GameObject m_currentTarget;

    [Tooltip("The current direction that the turret is aiming in.")]
    public Vector3 m_aimingDirection;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        m_aimingDirection = new Vector3( 0, 0, m_maxRotation - m_minRotation);

        m_turretGun.transform.eulerAngles = m_aimingDirection;

        m_fieldOfView.SetAimDirection( m_aimingDirection );

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
        }

        m_fieldOfView.SetOrigin( transform.position );

    }

    protected virtual void AimAtTarget( )
    {
        //Calculates the direction for the weapon to point based on the positions of the mouse and the object
        m_aimingDirection = m_currentTarget.transform.position - m_turretGun.transform.position;

        m_fieldOfView.SetAimDirection( m_aimingDirection );
        m_fieldOfView.SetOrigin( transform.position );

        //Calculates the angle using the x and y of the direction vector, converting it to degrees for use in a quaternion
        float angle = Mathf.Atan2( m_aimingDirection.x, m_aimingDirection.y ) * Mathf.Rad2Deg;

        //Calculates the rotation around the Z axis using the angle
        Quaternion rotation = Quaternion.AngleAxis( angle, Vector3.back );

        //Sets the rotation of the weapon to the rotation calculated above
        m_turretGun.transform.rotation = rotation;

    }

    public void SearchForTargets( )
    {
        if ( !m_rotatingLeft )
        {
            m_turretGun.transform.rotation = Quaternion.Lerp( transform.rotation , new Quaternion(0, 0, m_maxRotation, 0) , m_rotationSpeed * Time.deltaTime );
            if( Mathf.Approximately( m_turretGun.transform.rotation.z, m_maxRotation ) )
            {
                m_rotatingLeft = true;
            }
        }
        else
        {
            m_turretGun.transform.rotation = Quaternion.Lerp( transform.rotation , new Quaternion( 0 , 0 , m_minRotation , 0 ) , m_rotationSpeed * Time.deltaTime );
            if ( Mathf.Approximately( m_turretGun.transform.rotation.z , m_minRotation ) )
            {
                m_rotatingLeft = false;
            }

        }
    }

}
