using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMode : ActiveDroneBehaviourBase
{

    [SerializeField]
    private GameObject m_forceField;

    [SerializeField]
    private int m_shieldDuration;

    [SerializeField]
    private GameObject m_playerObject;

    public override void EnableModuleBehaviour( )
    {
        //Enables the forcefield object, which will block enemies from getting close to the player and deflect projectiles
        m_forceField.SetActive( true );

        //Disables the drone's basic behaviours so it can't follow the player or shoot at enemies
        m_droneController.DisableBasicBehaviours( );

        //Makes the player invulnerable, so they cannot take any damage while shielded
        m_playerObject.GetComponent<HealthManager>( ).m_isInvulnerable = true;

        //Starts the shield timer, which disables the shield after a certain amount of time
        StartCoroutine( ShieldTimer( ) );
    }

    public override void Update( )
    {
        base.Update( );

        //If the shield is active, then the drone's position is constantly set to that of the player so that the shield is cast around the player
        if ( m_behaviourActive )
        {
            transform.position = m_playerObject.transform.position;
        }
    }

    public IEnumerator ShieldTimer( )
    {

        m_behaviourActive = true;

        //Waits for the duration of shieldDuration before disabling the effect
        yield return new WaitForSeconds( m_shieldDuration );

        //Sets the behaviour to inactive so that it can be reactivated later
        m_behaviourActive = false;

        //Sets the force field object as inactive so that it no longer blocks incoming attacks
        m_forceField.SetActive( false );

        //Enables the basic behaviours of the drone so it can follow the player and shoot at enemies
        m_droneController.EnableBasicBehaviours( );

        //Sets the player to no longer be invulnerable
        m_playerObject.GetComponent<HealthManager>( ).m_isInvulnerable = false;

        //Starts the cooldown timer
        StartCoroutine( CooldownTimer( ) );

    }

}
